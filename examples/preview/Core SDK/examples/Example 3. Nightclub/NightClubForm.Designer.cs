namespace CoreSdkExamples
{
    partial class NightClubForm
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
            this.numericUpDownNumberOfLights = new System.Windows.Forms.NumericUpDown();
            this.TableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.labelMusic = new System.Windows.Forms.Label();
            this.labelNumberOfLights = new System.Windows.Forms.Label();
            this.SoundBrowseButton = new System.Windows.Forms.Button();
            this.PartyButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumberOfLights)).BeginInit();
            this.TableLayoutPanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // numericUpDownNumberOfLights
            // 
            this.numericUpDownNumberOfLights.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDownNumberOfLights.Location = new System.Drawing.Point(155, 3);
            this.numericUpDownNumberOfLights.Name = "numericUpDownNumberOfLights";
            this.numericUpDownNumberOfLights.Size = new System.Drawing.Size(147, 20);
            this.numericUpDownNumberOfLights.TabIndex = 0;
            this.numericUpDownNumberOfLights.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // TableLayoutPanelMain
            // 
            this.TableLayoutPanelMain.ColumnCount = 2;
            this.TableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanelMain.Controls.Add(this.labelMusic, 0, 1);
            this.TableLayoutPanelMain.Controls.Add(this.numericUpDownNumberOfLights, 1, 0);
            this.TableLayoutPanelMain.Controls.Add(this.labelNumberOfLights, 0, 0);
            this.TableLayoutPanelMain.Controls.Add(this.SoundBrowseButton, 1, 1);
            this.TableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.TableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.TableLayoutPanelMain.Name = "TableLayoutPanelMain";
            this.TableLayoutPanelMain.RowCount = 2;
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanelMain.Size = new System.Drawing.Size(305, 47);
            this.TableLayoutPanelMain.TabIndex = 1;
            // 
            // labelMusic
            // 
            this.labelMusic.AutoSize = true;
            this.labelMusic.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelMusic.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMusic.Location = new System.Drawing.Point(97, 26);
            this.labelMusic.Margin = new System.Windows.Forms.Padding(3);
            this.labelMusic.Name = "labelMusic";
            this.labelMusic.Size = new System.Drawing.Size(52, 18);
            this.labelMusic.TabIndex = 2;
            this.labelMusic.Text = "Music:";
            this.labelMusic.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelNumberOfLights
            // 
            this.labelNumberOfLights.AutoSize = true;
            this.labelNumberOfLights.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelNumberOfLights.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNumberOfLights.Location = new System.Drawing.Point(29, 3);
            this.labelNumberOfLights.Margin = new System.Windows.Forms.Padding(3);
            this.labelNumberOfLights.Name = "labelNumberOfLights";
            this.labelNumberOfLights.Size = new System.Drawing.Size(120, 17);
            this.labelNumberOfLights.TabIndex = 1;
            this.labelNumberOfLights.Text = "Number of lights:";
            this.labelNumberOfLights.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // SoundBrowseButton
            // 
            this.SoundBrowseButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SoundBrowseButton.Location = new System.Drawing.Point(155, 26);
            this.SoundBrowseButton.Name = "SoundBrowseButton";
            this.SoundBrowseButton.Size = new System.Drawing.Size(147, 18);
            this.SoundBrowseButton.TabIndex = 3;
            this.SoundBrowseButton.UseVisualStyleBackColor = true;
            this.SoundBrowseButton.Click += new System.EventHandler(this.BrowseMusic);
            // 
            // PartyButton
            // 
            this.PartyButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PartyButton.Location = new System.Drawing.Point(0, 270);
            this.PartyButton.Name = "PartyButton";
            this.PartyButton.Size = new System.Drawing.Size(305, 23);
            this.PartyButton.TabIndex = 1;
            this.PartyButton.Text = "Let\'s party";
            this.PartyButton.UseVisualStyleBackColor = true;
            this.PartyButton.Click += new System.EventHandler(this.LetsParty);
            // 
            // NightClubForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 293);
            this.Controls.Add(this.TableLayoutPanelMain);
            this.Controls.Add(this.PartyButton);
            this.Name = "NightClubForm";
            this.Text = "NightClub";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumberOfLights)).EndInit();
            this.TableLayoutPanelMain.ResumeLayout(false);
            this.TableLayoutPanelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDownNumberOfLights;
        private System.Windows.Forms.TableLayoutPanel TableLayoutPanelMain;
        private System.Windows.Forms.Label labelNumberOfLights;
        private System.Windows.Forms.Button PartyButton;
        private System.Windows.Forms.Label labelMusic;
        private System.Windows.Forms.Button SoundBrowseButton;
    }
}