using System;
using System.Diagnostics;
using System.Windows.Forms;

using ViewerSdk = vrcontext.walkinside.sdk;

namespace CoreSdkExamples
{
    /// <summary>
    /// A simple Viewer SDK plug-in that makes use of lights
    /// </summary>
    public class ExamplePlugin : ViewerSdk.IVRPlugin
    {
        static ViewerSdk.VRPluginDescriptor descriptor = new ViewerSdk.VRPluginDescriptor(
            shortDescription: "Core SDK Example 3: NightClub",
            longDescription: "Demonstrates lights.",
            type: ViewerSdk.VRPluginType.Unknown,
            version: 1,
            group: string.Empty,
            compileDate: "10/08/2016",
            license: "Vrcontext_SDK");

        static public ViewerSdk.VRPluginDescriptor GetDescriptor
        {
            get { return descriptor; }
        }

        public ViewerSdk.VRPluginDescriptor Description
        {
            get { return descriptor; }
        }

        public bool CreatePlugin(ViewerSdk.IVRViewerSdk viewerSdk)
        {
            this.viewerSdk = viewerSdk;

            pCommand = viewerSdk.UiCommandManager.RegisterPluginMenuCommand(
                getNames: () => new[] { "NightClub" },
                execute: () =>
                {
                    pForm = new NightClubForm(viewerSdk)
                    {
                        DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight
                    };
                    pForm.Show();
                    pForm.Closing += (o, e) => { pForm = null; };
                },
                getState: () => pForm == null ? ViewerSdk.VRCommandState.Available : ViewerSdk.VRCommandState.Disabled);
            return true;
        }

        public bool DestroyPlugin(ViewerSdk.IVRViewerSdk viewerSdk)
        {
            pCommand.Unregister();
            pForm?.Dispose();

            return true;
        }

        ViewerSdk.IVRViewerSdk viewerSdk;
        private ViewerSdk.IVRRegisteredCommand pCommand;
        private NightClubForm pForm;
    }
}
