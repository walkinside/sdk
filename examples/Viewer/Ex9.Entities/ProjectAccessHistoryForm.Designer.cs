namespace WIExample
{
    partial class ProjectAccessHistoryForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbx_ProjectAccessHistoryRecordsListBox = new System.Windows.Forms.ListBox();
            this.btnDeleteAll = new System.Windows.Forms.Button();
            this.btnDeleteEntry = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbx_ProjectAccessHistoryRecordsListBox
            // 
            this.lbx_ProjectAccessHistoryRecordsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbx_ProjectAccessHistoryRecordsListBox.FormattingEnabled = true;
            this.lbx_ProjectAccessHistoryRecordsListBox.ItemHeight = 16;
            this.lbx_ProjectAccessHistoryRecordsListBox.Location = new System.Drawing.Point(0, 0);
            this.lbx_ProjectAccessHistoryRecordsListBox.Margin = new System.Windows.Forms.Padding(4);
            this.lbx_ProjectAccessHistoryRecordsListBox.Name = "lbx_ProjectAccessHistoryRecordsListBox";
            this.lbx_ProjectAccessHistoryRecordsListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbx_ProjectAccessHistoryRecordsListBox.Size = new System.Drawing.Size(414, 505);
            this.lbx_ProjectAccessHistoryRecordsListBox.TabIndex = 0;
            this.lbx_ProjectAccessHistoryRecordsListBox.SelectedIndexChanged += new System.EventHandler(this.m_ProjectAccessHistoryRecordsListBox_SelectedIndexChanged);
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnDeleteAll.Location = new System.Drawing.Point(0, 535);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(414, 30);
            this.btnDeleteAll.TabIndex = 1;
            this.btnDeleteAll.Text = "Delete All";
            this.btnDeleteAll.UseVisualStyleBackColor = true;
            this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);
            // 
            // btnDeleteEntry
            // 
            this.btnDeleteEntry.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnDeleteEntry.Enabled = false;
            this.btnDeleteEntry.Location = new System.Drawing.Point(0, 505);
            this.btnDeleteEntry.Name = "btnDeleteEntry";
            this.btnDeleteEntry.Size = new System.Drawing.Size(414, 30);
            this.btnDeleteEntry.TabIndex = 2;
            this.btnDeleteEntry.Text = "Delete Selection";
            this.btnDeleteEntry.UseVisualStyleBackColor = true;
            this.btnDeleteEntry.Click += new System.EventHandler(this.btnDeleteRecord_Click);
            // 
            // ProjectAccessHistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 565);
            this.Controls.Add(this.lbx_ProjectAccessHistoryRecordsListBox);
            this.Controls.Add(this.btnDeleteEntry);
            this.Controls.Add(this.btnDeleteAll);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ProjectAccessHistoryForm";
            this.Text = "Project Access History";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbx_ProjectAccessHistoryRecordsListBox;
        private System.Windows.Forms.Button btnDeleteAll;
        private System.Windows.Forms.Button btnDeleteEntry;
    }
}