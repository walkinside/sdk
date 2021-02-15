namespace CoreSdkExamples
{
    partial class BrowsersForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrowsersForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ButtonAdd = new System.Windows.Forms.ToolStripButton();
            this.ButtonRemove = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ListBrowsers = new System.Windows.Forms.ListBox();
            this.PropertyGridBrowser = new System.Windows.Forms.PropertyGrid();
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ButtonAdd,
            this.ButtonRemove});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // ButtonAdd
            // 
            this.ButtonAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.ButtonAdd, "ButtonAdd");
            this.ButtonAdd.Name = "ButtonAdd";
            this.ButtonAdd.Image = global::vrcontext.walkinside.ui.icons.BlueJelly.Add32x32;
            this.ButtonAdd.Click += new System.EventHandler(this.AddBrowser);
            // 
            // ButtonRemove
            // 
            this.ButtonRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.ButtonRemove, "ButtonRemove");
            this.ButtonRemove.Name = "ButtonRemove";
            this.ButtonRemove.Image = global::vrcontext.walkinside.ui.icons.BlueJelly.Remove32x32;
            this.ButtonRemove.Click += new System.EventHandler(this.RemoveBrowser);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.ListBrowsers, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.PropertyGridBrowser, 0, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // ListBrowsers
            // 
            resources.ApplyResources(this.ListBrowsers, "ListBrowsers");
            this.ListBrowsers.FormattingEnabled = true;
            this.ListBrowsers.Name = "ListBrowsers";
            this.ListBrowsers.SelectedIndexChanged += new System.EventHandler(this.OnSelectionChanged);
            // 
            // PropertyGridBrowser
            // 
            resources.ApplyResources(this.PropertyGridBrowser, "PropertyGridBrowser");
            this.PropertyGridBrowser.Name = "PropertyGridBrowser";
            // 
            // BrowsersForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "BrowsersForm";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton ButtonAdd;
        private System.Windows.Forms.ToolStripButton ButtonRemove;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox ListBrowsers;
        private System.Windows.Forms.PropertyGrid PropertyGridBrowser;
    }
}
