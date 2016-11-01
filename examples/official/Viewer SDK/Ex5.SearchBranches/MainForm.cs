using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using vrcontext.walkinside.sdk;

namespace WIExample
{
    public partial class MainForm : VRForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (m_Selection != null)
            {
                m_Selection.Clear();
                m_Selection.Dispose();
            }
            base.OnClosing(e);
        }

        IVRGroupBuilder m_Selection;
        private void mToolStripButton_Click(object sender, EventArgs e)
        {
            if (m_Selection == null)
                m_Selection = SDKViewer.GroupManager.CreateEmptyGroup();
            else
                m_Selection.Clear();

            m_RichTextBox.Clear();

            string searchtext = mToolStripTextBox.Text;
            var branches = SDKViewer.ProjectManager.CurrentProject.BranchManager.GetBranchesByNameFragmentAndKind(searchtext, VRBranchKind.Cad);
            foreach (IVRBranch branch in branches)
            {
                m_RichTextBox.Text += branch.Name;
                m_RichTextBox.Text += "\r\n";
                m_Selection.Add(branch);
            }

            m_Selection.Highlight(Color.FromArgb(255, 32, 255, 255));
            m_Selection.JumpTo();
        }
    }
}