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
            this.m_RichTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // m_RichTextBox
            // 
            this.m_RichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_RichTextBox.Location = new System.Drawing.Point(0, 0);
            this.m_RichTextBox.Name = "m_RichTextBox";
            this.m_RichTextBox.Size = new System.Drawing.Size(292, 272);
            this.m_RichTextBox.TabIndex = 0;
            this.m_RichTextBox.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 272);
            this.Controls.Add(this.m_RichTextBox);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox m_RichTextBox;
    }
}