using Comos.Walkinside.Common.Math;
using Comos.Walkinside.Viewer;
using System;
using System.Drawing;
using System.Linq;
using vrcontext.walkinside.sdk;

namespace WIExample
{
    class Plugin : IVRPlugin
    {
        internal static VRPluginDescriptor m_Descriptor = new VRPluginDescriptor(
            VRPluginType.Unknown,
            version: 1,
            group: "",
            compileDate: "12/10/2017",
            shortDescription: "Example 10: Commands",
            longDescription: "This can be used in tandem with walkinside-js example 3.",
            license: "Vrcontext_SDK");

        static public VRPluginDescriptor GetDescriptor
        {
            get
            {
                return m_Descriptor;
            }
        }

        public VRPluginDescriptor Description
        {
            get
            {
                return m_Descriptor;
            }
        }

        private IVRRegisteredCommand pCreateLabelMenuCommand;
        private IVRRegisteredCommand pChangeColorMenuCommand;
        private IVRViewerSdk m_Viewer = null;

        private IDisposable pCreateLabelCommand;
        private IDisposable pChangeColorCommand;
        private IDisposable pGetLabelTextsRequest;

        private const string Example10CreateLabelsCommand = "wi.viewersdk.example10.create-labels";
        private const string Example10ChangeColorCommand = "wi.viewersdk.example10.change-color";
        private const string Example10LabelClickedNotification = "wi.viewersdk.example10.label-clicked";
        private const string Example10LabelTextsRequest = "wi.viewersdk.example10.get-label-texts";

        private ILabelGroup pLabelGroup;

        public bool CreatePlugin(IVRViewerSdk viewer)
        {
            m_Viewer = viewer;

            pCreateLabelMenuCommand = viewer.UiCommandManager.RegisterPluginMenuCommand(
                getNames: () => new[] { "Example 10", "Create Labels" },
                execute: () =>
                {
                    viewer.ForPreview.CommandManager.ExecuteCommand(
                        commandName: Example10CreateLabelsCommand);
                },
                getState: () => VRCommandState.Available);

            pChangeColorMenuCommand = viewer.UiCommandManager.RegisterPluginMenuCommand(
                getNames: () => new[] { "Example 10", "Random Color" },
                execute: () =>
                {
                    Random r = new Random();
                    var color = Color.FromArgb(255, r.Next(256), r.Next(256), r.Next(256));

                    viewer.ForPreview.CommandManager.ExecuteCommand(
                        commandName: Example10ChangeColorCommand,
                        commandInput: ColorTranslator.ToHtml(color));
                },
                getState: () => VRCommandState.Available);


            pCreateLabelCommand = viewer.ForPreview.CommandManager.RegisterCommandHandler(
                commandName: Example10CreateLabelsCommand,
                handler: (input) =>
                {
                    var actorPosition = m_Viewer.CurrentActor.Position;

                    pLabelGroup?.Dispose();
                    pLabelGroup = m_Viewer.LabelManager.CreateLabelGroup();
                    pLabelGroup.Add("label 1", new Point3d(actorPosition.X, actorPosition.Y, actorPosition.Z));
                    pLabelGroup.Add("label 2", new Point3d(actorPosition.X + 10.0, actorPosition.Y, actorPosition.Z));
                    pLabelGroup.Add("label 3", new Point3d(actorPosition.X, actorPosition.Y, actorPosition.Z + 10.0));
                    pLabelGroup.Add("label 4", new Point3d(actorPosition.X, actorPosition.Y + 10.0, actorPosition.Z));
                });

            pChangeColorCommand = viewer.ForPreview.CommandManager.RegisterCommandHandler(
                commandName: Example10ChangeColorCommand,
                handler: (input) =>
                {
                    if (pLabelGroup != null)
                    {
                        pLabelGroup.Color = ColorTranslator.FromHtml((string)input);
                    }
                });

            pGetLabelTextsRequest = viewer.ForPreview.CommandManager.RegisterRequestHandler(
                requestName: Example10LabelTextsRequest,
                handler: (input) =>
                {
                    return pLabelGroup.Labels.Select(l => l.Text).ToArray();
                });

            viewer.LastCastRayResultChanged += Viewer_LastCastRayResultChanged;

            return true;
        }

        private void Viewer_LastCastRayResultChanged(object sender, CastRayResultEventArgs e)
        {
            var label = e.Result?.Label;
            if (label != null)
            {
                m_Viewer.ForPreview.NotificationManager.Notify(Example10LabelClickedNotification, label.Text);
            }
        }

        public bool DestroyPlugin(IVRViewerSdk viewer)
        {
            viewer.LastCastRayResultChanged -= Viewer_LastCastRayResultChanged;

            pCreateLabelMenuCommand.Unregister();
            pChangeColorMenuCommand.Unregister();

            pCreateLabelCommand.Dispose();
            pChangeColorCommand.Dispose();

            pGetLabelTextsRequest.Dispose();

            pLabelGroup.Dispose();

            return true;
        }
    }
}
