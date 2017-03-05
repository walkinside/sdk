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

            this.menuItem = this.viewerSdk.UI.PluginMenu.DropDownItems.Add("NightClub");

            this.viewerSdk.UI.RegisterVRFormWithMenu(Keys.None, (ToolStripMenuItem)menuItem, typeof(NightClubForm), this.viewerSdk);

            return true;
        }

        public bool DestroyPlugin(ViewerSdk.IVRViewerSdk viewerSdk)
        {
            this.menuItem.Dispose();

            this.viewerSdk.UI.UnregisterVRFORM((ToolStripMenuItem)menuItem, typeof(NightClubForm));

            return true;
        }

        ViewerSdk.IVRViewerSdk viewerSdk;
        ToolStripItem menuItem = null;
    }
}
