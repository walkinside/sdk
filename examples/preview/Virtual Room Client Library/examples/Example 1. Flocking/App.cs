using System;
using System.Linq;
using System.Diagnostics;
using System.Net;
using System.Collections.Generic;

using Comos.Walkinside.Its.Protocol;
using Comos.Walkinside.Its.Client;

namespace VirtualRoomClientLibraryExamples
{
    /// <summary>
    /// Simple console application that connects to a Virtual Room and
    /// spawns a number of bots. Bots will follow the first peer that looks
    /// like human, in circle formation.
    /// </summary>
    class App
    {
        static class ExitCode
        {
            public const int Success = 0;
            public const int InvalidArgs = -1;
            public const int UnexpectedError = 1;
        }

        static int Main(string[] args)
        {
            Console.WriteLine("Please input Walkinside project name:");
            string projectName = Console.ReadLine();
            
            var app = new App();
            try
            {
                app.Run(projectName: projectName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return ExitCode.UnexpectedError;
            }
            return ExitCode.Success;
        }

        class BotInfo
        {
            public int Index;
            public string Name;
            public uint? LeaderAvatarId;
            public ITrainingRoomClient Client;
            public double[] CurrentPosition = new double[3];
            public int CurrentAnimationId;
            public double[] CurrentRotation = new double[16];
        }

        App()
        {
            // Prepare list of bots.
            // Note: The total number of bots and avatars in the viewer should not 
            // exceed 16; this means that we can have at most 15 bots when there is
            // already 1 leader avatar in the viewer. If there are more avatars in viewer,
            // we should have less bots.
            var names = new[]
            {
                "Tony", "Suit", "Inspector", "Lara", "Sam", "John", "Henry",
                "Kaixin" , "Heidi", "Trainee10", 
                //"Trainee11", "Trainee12","Trainee13","Trainee14","Trainee15"
            };
            this.botsByName = names
                .Select((name, i) => new BotInfo
                {
                    Index = i,
                    Name = string.Format("{0} (bot #{1})", name, i),
                })
                .ToDictionary(bot => bot.Name, bot => bot);
        }

        void Run(string projectName)
        {
            var roomAddress = new IPEndPoint(
                IPAddress.Parse("127.0.0.1"), port: 1080);
            var discoveryClient = new DiscoveryClient(roomAddress); 
            discoveryClient.TrainingRoomClientConnecting += this.discoveryClient_TrainingRoomClientConnecting;

            // Join bots one by one.

            foreach (var bot in this.botsByName.Values)
            {
                this.currentlyJoiningBot = bot;
                var client = discoveryClient
                    .JoinRoomAsync(
                        roomAddress.Port,
                        asInstructor: false,
                        avatarName: bot.Name,
                        projectName: projectName,
                        peerTag: "WalkinsideViewer/10.2.33",
                        // We only have 8 characters in Walkinside viewer.
                        avatarModelName: string.Format("char{0}", bot.Index % 8), 
                        avatarSkinName: "skin0",
                        gravityOn: false,
                        pathOn: false)
                    .GetAwaiter()
                    .GetResult();
                if (client == null)
                {
                    Console.WriteLine("Failed to create bot #{0}", bot.Index);
                    return;
                }
            }
            this.currentlyJoiningBot = null;

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();

            foreach (var bot in this.botsByName.Values)
            {
                // There are 2 steps for properly disconnecting from server.
                // When either of them is not called, clients need to wait for virtual
                // room server to clean up the virtual room state.
                using (bot.Client)
                {
                    // Step 1: call Logout to unregister from server.
                    bot.Client.Logout();
                } // Step 2: bot.Client.Dispose will close the underlying connection.
            }
        }

        void discoveryClient_TrainingRoomClientConnecting(
            object sender,
            TrainingRoomClientConnectingEventArgs connectingEventArgs)
        {
            // Each bot will watch first user in the room and follow it
            // around, keeping away at certain distance and angle.

            var client = connectingEventArgs.Client;
            var self = this.currentlyJoiningBot;

            self.Client = client;

            client.CommunicationErrorIssued += (_, eventArgs) =>
            {
                Console.WriteLine(
                    "{0} | Communication error {1}:\n{2}",
                    self.Name,
                    eventArgs.ErrorCode,
                    eventArgs.Error);
            };

            client.LoggedIn += (_, eventArgs) =>
            {
                Console.WriteLine(
                    "{0} | Login result: {1}",
                    self.Name,
                    eventArgs.StatusCode);
            };

            client.AvatarAdded += (_, eventArgs) =>
            {
                if (self.LeaderAvatarId.HasValue)
                {
                    return; // Already have a leader.
                }

                // Need a leader.
                var name = eventArgs.AvatarName;
                Debug.Assert(!string.IsNullOrWhiteSpace(name)); 
                if (name.Contains("(bot #"))
                {
                    return; // Looks like a bot, cannot be my leader.
                }

                self.LeaderAvatarId = eventArgs.AvatarId;
                Console.WriteLine("{0} | Now following {1}...", self.Name, name);
            };

            client.AvatarRemoved += (_, eventArgs) =>
            {
                if (self.LeaderAvatarId == eventArgs.AvatarId)
                {
                    self.LeaderAvatarId = null; // Lost the leader.
                    Console.WriteLine("{0} | Lost leader.", self.Name);
                }
            };

            client.AvatarStateUpdated += (_, eventArgs) =>
            {
                const int distanceFromLeader = 4;

                var myIndex = self.Index;
                var botCount = this.botsByName.Count;

                if (eventArgs.AvatarId != self.LeaderAvatarId)
                {
                    return;
                }

                
                bool shouldSendUpdate = false;
                if (eventArgs.IsPositionChanged)
                {
                    var xDelta = distanceFromLeader * Math.Cos(myIndex * (2 * Math.PI / botCount));
                    var zDelta = distanceFromLeader * Math.Sin(myIndex * (2 * Math.PI / botCount));
                    var positon = eventArgs.Position;
                    var newPosition = new[]
                    {
                        positon[0] + xDelta,
                        positon[1],
                        positon[2] + zDelta,
                    };
                    self.CurrentPosition = newPosition;

                    shouldSendUpdate = true;
                }

                if (eventArgs.IsAnimationChanged)
                {
                    self.CurrentAnimationId = eventArgs.AnimationId;
                    shouldSendUpdate = true;
                }

                if (eventArgs.IsRotationChanged)
                {
                    self.CurrentRotation = eventArgs.Rotation;
                    shouldSendUpdate = true;
                }

                if (shouldSendUpdate)
                {
                    client.PostStateUpdateAsync(
                        newPosition: self.CurrentPosition,
                        newAnimationId: self.CurrentAnimationId,
                        newRotation: self.CurrentRotation);
                }
            };

            client.LoggedOut += (_, eventArgs) =>
            {
                Console.WriteLine("{0} | Logged out.", self.Name);
            };

            client.Disconnected += (_, eventArgs) =>
            {
                Console.WriteLine("{0} | Disconnected.", self.Name);
            };
        }

        readonly IDictionary<string, BotInfo> botsByName;
        BotInfo currentlyJoiningBot;
    }
}
