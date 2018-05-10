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

        ToolStripMenuItem m_ToolStripItem;
        private IVRViewerSdk m_Viewer = null;

        public bool CreatePlugin(IVRViewerSdk viewer)
        {
            m_Viewer = viewer;
            m_ToolStripItem = viewer.UI.PluginMenu.DropDownItems.Add(m_Descriptor.ShortDescription) as ToolStripMenuItem;
            viewer.UI.RegisterVRFormWithMenu(Keys.NoName, m_ToolStripItem, typeof(ProjectAccessHistoryForm));

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
            viewer.UI.UnregisterVRFORM(m_ToolStripItem, typeof(ProjectAccessHistoryForm));
            viewer.UI.PluginMenu.DropDownItems.Remove(m_ToolStripItem);
            m_ToolStripItem = null;

            return true;
        }
    }
}
