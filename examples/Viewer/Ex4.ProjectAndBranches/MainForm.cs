using Comos.Walkinside.Common.Branches;
using Comos.Walkinside.Viewer;
using System.ComponentModel;
using System.Linq;

using vrcontext.walkinside.sdk;

namespace WIExample
{
    public partial class MainForm : VRForm
    {
        public MainForm()
        {
            InitializeComponent();
            // Attach a listener to detect when a CAD hierarchy branch is selected. This is also triggered for FRT branches.
            SDKViewer.Selection.Changed += Selection_Changed;
            // Attach a listener when a user opens a new Walkinside model.
            SDKViewer.ProjectManager.OnProjectOpen += new VRProjectEventHandler(ProjectManager_OnProjectOpen);
            // Attach a listener when a user closes the Walkinside model.
            SDKViewer.ProjectManager.OnProjectClose += new VRProjectEventHandler(ProjectManager_OnProjectClose);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            // Remove a listener to detect when a CAD hierarchy branch is selected. This also is triggered for FRT branches.
            SDKViewer.Selection.Changed -= Selection_Changed;
            // Remove a listener when a user opens a new Walkinside model.
            SDKViewer.ProjectManager.OnProjectOpen -= new VRProjectEventHandler(ProjectManager_OnProjectOpen);
            // Remove a listener when a user closes the Walkinside model.
            SDKViewer.ProjectManager.OnProjectClose -= new VRProjectEventHandler(ProjectManager_OnProjectClose);
            base.OnClosing(e);
        }

        void ProjectManager_OnProjectClose(object sender, VRProjectEventArgs e)
        {
            // When the user closes a new model the text in the rich text box is updated.
            // Note this is almost not noticeable, as due to the layout opening a project could trigger closing of this form.
            m_RichTextBox.Text = "Closed the project = " + e.Project.Name;
        }

        void ProjectManager_OnProjectOpen(object sender, VRProjectEventArgs e)
        {
            // When the user opens a new model the text in the rich text box is updated.
            // Note this is almost not noticeable, as due to the layout opening a project could trigger closing of this form.
            m_RichTextBox.Text = "Opened a new project = " + e.Project.Name;
        }

        void Selection_Changed(object sender, SelectionChangedEventArgs e)
        {
            var branches = e.Items.Branches().ToArray();
            // Just to be 100% sure ignore when branches array is a null pointer.

            // This variable will contain a null pointer, or the instance corresponding with the 
            // CAD Hierarchy branch selected by the user.
            IVRBranch branch_cad = null;

            // Iterate all the selected branches (normally 0, CAD or FRT, or both CAD and FRT)
            foreach (IVRBranch branch in branches)
            {
                // If branch.Kind is greater than VRBranchKind.Cad, then it is not CAD. Probably it is FRT.
                if (branch.Kind > BranchKind.Cad)
                {
                    continue;
                }
                // Assign branch_cad to the found instance.
                branch_cad = branch;
            }

            // Test that a Branch for the CAD branch was found. 
            // (Could be there was no CAD selected but only a FRT) 
            if (branch_cad == null)
            {
                m_RichTextBox.Text = "No Cad Hierarchy branch selected.";
                return;
            }

            // Set the text of the rich textbox equal to the name of the branch.
            m_RichTextBox.Text = branch_cad.Name;
        }
    }
}
