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
            this.BtnColor = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TrckBarTransparancy = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.CheckBlinking = new System.Windows.Forms.CheckBox();
            this.BtnSearch = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.TrckBarTransparancy)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnColor
            // 
            this.BtnColor.BackColor = System.Drawing.Color.Yellow;
            this.BtnColor.Location = new System.Drawing.Point(65, 44);
            this.BtnColor.Name = "BtnColor";
            this.BtnColor.Size = new System.Drawing.Size(75, 23);
            this.BtnColor.TabIndex = 1;
            this.BtnColor.UseVisualStyleBackColor = false;
            this.BtnColor.Click += new System.EventHandler(this.BtnColor_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Color";
            // 
            // TrckBarTransparancy
            // 
            this.TrckBarTransparancy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TrckBarTransparancy.LargeChange = 8;
            this.TrckBarTransparancy.Location = new System.Drawing.Point(114, 73);
            this.TrckBarTransparancy.Maximum = 255;
            this.TrckBarTransparancy.Name = "TrckBarTransparancy";
            this.TrckBarTransparancy.Size = new System.Drawing.Size(425, 56);
            this.TrckBarTransparancy.TabIndex = 3;
            this.TrckBarTransparancy.TickFrequency = 16;
            this.TrckBarTransparancy.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.TrckBarTransparancy.ValueChanged += new System.EventHandler(this.TrckBarTransparancy_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Search";
            // 
            // textBox1
            // 
            this.textBox1.AcceptsReturn = true;
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(65, 13);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(396, 22);
            this.textBox1.TabIndex = 6;
            this.textBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUp);
            // 
            // CheckBlinking
            // 
            this.CheckBlinking.AutoSize = true;
            this.CheckBlinking.Location = new System.Drawing.Point(65, 145);
            this.CheckBlinking.Name = "CheckBlinking";
            this.CheckBlinking.Size = new System.Drawing.Size(60, 21);
            this.CheckBlinking.TabIndex = 7;
            this.CheckBlinking.Text = "Blink";
            this.CheckBlinking.UseVisualStyleBackColor = true;
            this.CheckBlinking.CheckedChanged += new System.EventHandler(this.CheckBlinking_CheckedChanged);
            // 
            // BtnSearch
            // 
            this.BtnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSearch.Location = new System.Drawing.Point(467, 13);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(75, 23);
            this.BtnSearch.TabIndex = 8;
            this.BtnSearch.Text = "Search";
            this.BtnSearch.UseVisualStyleBackColor = true;
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Transparancy";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 299);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.BtnSearch);
            this.Controls.Add(this.CheckBlinking);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TrckBarTransparancy);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnColor);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.TrckBarTransparancy)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar TrckBarTransparancy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox CheckBlinking;
        private System.Windows.Forms.Button BtnSearch;
        private System.Windows.Forms.Label label3;
    }
}