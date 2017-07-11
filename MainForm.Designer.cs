namespace HoxHudOneClickInstall
{
    partial class HoxHudOCI
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
            this.InstallBtn = new System.Windows.Forms.Button();
            this.GameFolderDiag = new System.Windows.Forms.FolderBrowserDialog();
            this.downloadProgressBar = new System.Windows.Forms.ProgressBar();
            this.keepTweakDataChkBtn = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // InstallBtn
            // 
            this.InstallBtn.BackColor = System.Drawing.SystemColors.Control;
            this.InstallBtn.Location = new System.Drawing.Point(0, 0);
            this.InstallBtn.Name = "InstallBtn";
            this.InstallBtn.Size = new System.Drawing.Size(262, 200);
            this.InstallBtn.TabIndex = 0;
            this.InstallBtn.Text = "Install";
            this.InstallBtn.UseVisualStyleBackColor = false;
            this.InstallBtn.Click += new System.EventHandler(this.InstallBtn_Click);
            // 
            // GameFolderDiag
            // 
            this.GameFolderDiag.SelectedPath = "C:\\";
            this.GameFolderDiag.ShowNewFolderButton = false;
            // 
            // downloadProgressBar
            // 
            this.downloadProgressBar.Location = new System.Drawing.Point(0, 197);
            this.downloadProgressBar.Name = "downloadProgressBar";
            this.downloadProgressBar.Size = new System.Drawing.Size(262, 26);
            this.downloadProgressBar.TabIndex = 1;
            // 
            // keepTweakDataChkBtn
            // 
            this.keepTweakDataChkBtn.AutoSize = true;
            this.keepTweakDataChkBtn.Checked = true;
            this.keepTweakDataChkBtn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.keepTweakDataChkBtn.Location = new System.Drawing.Point(12, 10);
            this.keepTweakDataChkBtn.Name = "keepTweakDataChkBtn";
            this.keepTweakDataChkBtn.Size = new System.Drawing.Size(110, 17);
            this.keepTweakDataChkBtn.TabIndex = 2;
            this.keepTweakDataChkBtn.Text = "Keep TweakData";
            this.keepTweakDataChkBtn.UseVisualStyleBackColor = true;
            this.keepTweakDataChkBtn.Visible = false;
            // 
            // HoxHudOCI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 199);
            this.Controls.Add(this.keepTweakDataChkBtn);
            this.Controls.Add(this.downloadProgressBar);
            this.Controls.Add(this.InstallBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HoxHudOCI";
            this.ShowIcon = false;
            this.Text = "HoxHud Installer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HoxHudOCI_FormClosing);
            this.Shown += new System.EventHandler(this.HoxHudOCI_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button InstallBtn;
        private System.Windows.Forms.FolderBrowserDialog GameFolderDiag;
        private System.Windows.Forms.ProgressBar downloadProgressBar;
        private System.Windows.Forms.CheckBox keepTweakDataChkBtn;
    }
}

