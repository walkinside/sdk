using System;
using System.Collections.Generic;
using System.Linq;
using vrcontext.walkinside.sdk;

namespace WIExample
{
    public partial class ProjectAccessHistoryForm : VRForm
    {
        private readonly ProjectAccessHistory m_history;

        public ProjectAccessHistoryForm()
        {
            InitializeComponent();

            m_history = new ProjectAccessHistory(SDKViewer.ProjectManager.CurrentProject.EntityManager);

            PopulateRecords();
        }

        private void PopulateRecords()
        {
            lbx_ProjectAccessHistoryRecordsListBox.Items.Clear();

            foreach (var ProjectAccessHistoryRecord in m_history.GetRecords())
            {
                lbx_ProjectAccessHistoryRecordsListBox.Items.Add(ProjectAccessHistoryRecord);
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            m_history.DeleteAllRecords();
            lbx_ProjectAccessHistoryRecordsListBox.Items.Clear();
        }

        private void btnDeleteRecord_Click(object sender, EventArgs e)
        {
            IEnumerable<ProjectAccessHistoryRecord> multientry = lbx_ProjectAccessHistoryRecordsListBox.SelectedItems.Cast<ProjectAccessHistoryRecord>().ToList();
            m_history.DeleteRecords(multientry);
            PopulateRecords();
        }

        private void m_ProjectAccessHistoryRecordsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnDeleteEntry.Enabled = (lbx_ProjectAccessHistoryRecordsListBox.SelectedItems.Count != 0);
        }
    }
}
