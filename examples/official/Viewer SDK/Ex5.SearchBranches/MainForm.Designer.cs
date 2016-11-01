namespace WIExample
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.m_RichTextBox = new System.Windows.Forms.RichTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.mToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.mToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.mToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_RichTextBox
            // 
            this.m_RichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_RichTextBox.Location = new System.Drawing.Point(0, 25);
            this.m_RichTextBox.Name = "m_RichTextBox";
            this.m_RichTextBox.Size = new System.Drawing.Size(292, 247);
            this.m_RichTextBox.TabIndex = 0;
            this.m_RichTextBox.Text = "";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mToolStripLabel,
            this.mToolStripTextBox,
            this.mToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(292, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // mToolStripLabel
            // 
            this.mToolStripLabel.Name = "mToolStripLabel";
            this.mToolStripLabel.Size = new System.Drawing.Size(29, 22);
            this.mToolStripLabel.Text = "Text";
            // 
            // mToolStripTextBox
            // 
            this.mToolStripTextBox.Name = "mToolStripTextBox";
            this.mToolStripTextBox.Size = new System.Drawing.Size(150, 25);
            // 
            // mToolStripButton
            // 
            this.mToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("mToolStripButton.Image")));
            this.mToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mToolStripButton.Name = "mToolStripButton";
            this.mToolStripButton.Size = new System.Drawing.Size(73, 22);
            this.mToolStripButton.Text = "Start Search";
            this.mToolStripButton.Click += new System.EventHandler(this.mToolStripButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 272);
            this.Controls.Add(this.m_RichTextBox);
            this.Controls.Add(this.toolStrip1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox m_RichTextBox;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel mToolStripLabel;
        private System.Windows.Forms.ToolStripTextBox mToolStripTextBox;
        private System.Windows.Forms.ToolStripButton mToolStripButton;
    }
}