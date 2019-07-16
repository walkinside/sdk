using System;
using System.Linq;
using System.Drawing;

using ViewerSdk = vrcontext.walkinside.sdk;
using CoreSdk = vrcontext.walkinside.sdk.Preview.Core;

namespace CoreSdkExamples
{
    sealed class CountdownModel : CoreSdk.Model
    {
        public CountdownModel(
            CoreSdk.ModelManager modelManager,
            int startingNumber,
            string finalMessage)
            : base(modelManager)
        {
            const double messageOverlapDurationInSeconds = 1.3;
            const double numberMessageDurationInSeconds = 1 + messageOverlapDurationInSeconds;

            this.messages = new MessageInfo[startingNumber + 1];

            for (int n = startingNumber, i = 0; n > 0; --n, ++i)
            {
                var m = new MessageInfo
                {
                    Text = n.ToString(),
                    StartTime = TimeSpan.FromSeconds(
                        i * (numberMessageDurationInSeconds - messageOverlapDurationInSeconds)),
                    Duration = TimeSpan.FromSeconds(numberMessageDurationInSeconds),
                    Color = Color.Crimson,
                };

                this.messages[i] = m;
            }
            var finalMessageInfo = new MessageInfo
            {
                Text = finalMessage,
                StartTime = TimeSpan.FromSeconds(
                    startingNumber * (numberMessageDurationInSeconds - messageOverlapDurationInSeconds)),
                Duration = TimeSpan.FromSeconds(3),
                Color = Color.Lime,
            };
            this.messages[this.messages.Length - 1] = finalMessageInfo;

            this.animDuration
                = this.messages.Max(m => m.EndTime)
                - this.messages.Min(m => m.StartTime)
                + TimeSpan.FromSeconds(3); // 3 silent seconds in the end.

            // We will use proportional font, 512 pixels high - big enough to
            // look good even in full-screen.
            this.fontFace = new CoreSdk.FontFace("Times New Roman", height: 512);

            this.Subscribe(
                CoreSdk.PassSubscription.Overlay,
                CoreSdk.StageSubscription.Text);
            this.Setup();
        }

        class MessageInfo
        {
            public string Text = string.Empty;
            public TimeSpan StartTime; // Relative to animation sequence start.
            public TimeSpan Duration;
            public Color Color = Color.Black;

            public TimeSpan EndTime
            {
                get { return this.StartTime + this.Duration; }
            }
        }

        public override void Update(float timeDelta)
        {
            this.simTime = this.simTime.Add(TimeSpan.FromSeconds(timeDelta));
        }

        public override void Render(CoreSdk.RenderingContext context)
        {
            // Start with line height equal to 100% of viewport height and
            // shrink to 10%.
            var startHeight = context.Viewport.Height;
            var endHeight = context.Viewport.Height * 0.1;

            // Get time relative to animation start.
            var animTime = TimeSpan.FromSeconds(
                this.simTime.TotalSeconds % this.animDuration.TotalSeconds);

            using (context.Renderer.SetRenderingMode(CoreSdk.RenderingMode.Default2d))
            {
                foreach (var m in this.messages)
                {
                    if (animTime < m.StartTime || animTime > m.EndTime)
                    {
                        continue; // This message should not be displayed now.
                    }

                    // Get time relative to message's time window.
                    var messageAnimTime = animTime - m.StartTime;
                    // Normalize to [0..1].
                    var messageAnimTimeNorm = messageAnimTime.TotalSeconds / m.Duration.TotalSeconds;

                    var desiredHeight = Lerp(startHeight, endHeight, messageAnimTimeNorm);
                    var scale = (float)GetScaleForDesiredSize(
                        size: this.fontFace.GetHeight(),
                        desiredSize: desiredHeight);

                    // Center message in viewport.
                    var textDim = context.Renderer.GetTextDimension(this.fontFace, m.Text, scale);
                    var position = new CoreSdk.Vec2f(
                        x: (context.Viewport.Width - textDim.X) / 2,
                        y: (context.Viewport.Height - textDim.Y) / 2);

                    // Red text fading out gradually.
                    var opaqueness = (float)Lerp(1, 0, messageAnimTimeNorm);
                    var color = new CoreSdk.Vec4f(
                        m.Color.R / 255f,
                        m.Color.G / 255f,
                        m.Color.B / 255f,
                        opaqueness);

                    context.Renderer.Draw(this.fontFace, color, position, m.Text, scale);
                }
            }
        }

        static double GetScaleForDesiredSize(double size, double desiredSize)
        {
            return desiredSize * 300 / size;
        }

        static double Lerp(double start, double end, double t)
        {
            return start * (1 - t) + end * t;
        }

        readonly CoreSdk.FontFace fontFace;
        readonly MessageInfo[] messages;
        readonly TimeSpan animDuration;
        TimeSpan simTime;
    }
}
