using System;
using System.Windows.Forms;
using vrcontext.walkinside.sdk;

namespace WIExample
{
    class ProjectAccessHistoryPlugin : IVRPlugin
    {
        internal static VRPluginDescriptor m_Descriptor = new VRPluginDescriptor(
            VRPluginType.Unknown, 
            version: 1,
            group: "", 
            compileDate: "12/10/2017",
            shortDescription: "Example 9: Project Access History",
            longDescription: "Uses entities to persistently store the history of who opened the project",
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

        private IVRRegisteredCommand pCommand;
        private ProjectAccessHistoryForm pForm;
        private IVRViewerSdk m_Viewer = null;

        public bool CreatePlugin(IVRViewerSdk viewer)
        {
            m_Viewer = viewer;
            pCommand = viewer.CommandManager.RegisterPluginMenuCommand(
                getNames: () => new[] { "Example 8" },
                execute: () =>
                {
                    pForm = new ProjectAccessHistoryForm
                    {
                        DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight
                    };
                    pForm.Show();
                    pForm.Closing += (o, e) => { pForm = null; };
                },
                getState: () => pForm == null ? VRCommandState.Available : VRCommandState.Disabled);

            viewer.ProjectManager.OnProjectOpen += ProjectManager_OnProjectOpen;

            return true;
        }

        private void ProjectManager_OnProjectOpen(object sender, VRProjectEventArgs e)
        {
            var history = new ProjectAccessHistory(m_Viewer.ProjectManager.CurrentProject.EntityManager);
            var record = new ProjectAccessHistoryRecord
            {
                Date = DateTime.Now,
                UserName = System.Environment.UserName,
                ComputerName = System.Environment.MachineName,
            };
            history.AddRecord(record);
        }

        public bool DestroyPlugin(IVRViewerSdk viewer)
        {
            pCommand.Unregister();
            pForm?.Dispose();

            return true;
        }
    }
}
