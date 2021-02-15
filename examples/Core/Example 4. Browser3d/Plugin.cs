using System.Windows.Forms;
using vrcontext.walkinside.sdk;

namespace CoreSdkExamples
{
    class Plugin : IVRPlugin
    {
        internal static VRPluginDescriptor m_Descriptor = new VRPluginDescriptor(
            VRPluginType.Unknown,
            version: 1,
            group: "",
            compileDate: "12/10/2017",
            shortDescription: "Core SDK Example 4: 3d Browsers",
            longDescription: "Demonstrates 3d Browsers.",
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

        private IVRViewerSdk m_Viewer = null;
        private Form pForm;
        private IVRRegisteredCommand pOpenBrowserForm;


        public bool CreatePlugin(IVRViewerSdk viewer)
        {
            m_Viewer = viewer;

            pOpenBrowserForm = viewer.UiCommandManager.RegisterPluginMenuCommand(
                getNames: () => new[] { "Core SDK Example 4", "Create Browsers" },
                execute: () =>
                {
                    pForm = new BrowsersForm(viewer)
                    {
                        DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight
                    };
                    pForm.Show();
                    pForm.Closing += (o, e) => { pForm = null; };
                },
                getState: () => VRCommandState.Available);

            return true;
        }


        public bool DestroyPlugin(IVRViewerSdk viewer)
        {
            pOpenBrowserForm.Unregister();
            pForm?.Close();

            return true;
        }
    }
}
