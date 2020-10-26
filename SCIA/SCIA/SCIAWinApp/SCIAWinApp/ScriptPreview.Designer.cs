namespace SCIA
{
    partial class ScriptPreview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptPreview));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblPreview = new System.Windows.Forms.RichTextBox();
            this.grpContainer = new System.Windows.Forms.GroupBox();
            this.rbDeleteScript = new System.Windows.Forms.RadioButton();
            this.rbUninstallScript = new System.Windows.Forms.RadioButton();
            this.rbInstallScript = new System.Windows.Forms.RadioButton();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.grpContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.lblPreview);
            this.panel1.Location = new System.Drawing.Point(21, 144);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1469, 1404);
            this.panel1.TabIndex = 1;
            // 
            // lblPreview
            // 
            this.lblPreview.AutoWordSelection = true;
            this.lblPreview.Location = new System.Drawing.Point(28, 45);
            this.lblPreview.Name = "lblPreview";
            this.lblPreview.Size = new System.Drawing.Size(1397, 1306);
            this.lblPreview.TabIndex = 0;
            this.lblPreview.Text = "";
            // 
            // grpContainer
            // 
            this.grpContainer.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.grpContainer.Controls.Add(this.rbDeleteScript);
            this.grpContainer.Controls.Add(this.rbUninstallScript);
            this.grpContainer.Controls.Add(this.rbInstallScript);
            this.grpContainer.Location = new System.Drawing.Point(21, 38);
            this.grpContainer.Name = "grpContainer";
            this.grpContainer.Size = new System.Drawing.Size(1473, 76);
            this.grpContainer.TabIndex = 2;
            this.grpContainer.TabStop = false;
            // 
            // rbDeleteScript
            // 
            this.rbDeleteScript.AutoSize = true;
            this.rbDeleteScript.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbDeleteScript.Location = new System.Drawing.Point(1290, 30);
            this.rbDeleteScript.Name = "rbDeleteScript";
            this.rbDeleteScript.Size = new System.Drawing.Size(166, 29);
            this.rbDeleteScript.TabIndex = 2;
            this.rbDeleteScript.Text = "Delete Script";
            this.rbDeleteScript.UseVisualStyleBackColor = true;
            this.rbDeleteScript.Click += new System.EventHandler(this.rbDeleteScript_CheckedChanged);
            // 
            // rbUninstallScript
            // 
            this.rbUninstallScript.AutoSize = true;
            this.rbUninstallScript.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbUninstallScript.Location = new System.Drawing.Point(649, 31);
            this.rbUninstallScript.Name = "rbUninstallScript";
            this.rbUninstallScript.Size = new System.Drawing.Size(187, 29);
            this.rbUninstallScript.TabIndex = 1;
            this.rbUninstallScript.Text = "Uninstall Script";
            this.rbUninstallScript.UseVisualStyleBackColor = true;
            this.rbUninstallScript.Click += new System.EventHandler(this.rbUninstallScript_CheckedChanged);
            // 
            // rbInstallScript
            // 
            this.rbInstallScript.AutoSize = true;
            this.rbInstallScript.Checked = true;
            this.rbInstallScript.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbInstallScript.Location = new System.Drawing.Point(28, 31);
            this.rbInstallScript.Name = "rbInstallScript";
            this.rbInstallScript.Size = new System.Drawing.Size(160, 29);
            this.rbInstallScript.TabIndex = 0;
            this.rbInstallScript.TabStop = true;
            this.rbInstallScript.Text = "Install Script";
            this.rbInstallScript.UseVisualStyleBackColor = true;
            this.rbInstallScript.Click += new System.EventHandler(this.rbInstallScript_CheckedChanged);
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblStatus.Location = new System.Drawing.Point(26, 1605);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(948, 38);
            this.lblStatus.TabIndex = 53;
            this.lblStatus.Text = "Happy Sitecoring!";
            // 
            // ScriptPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1523, 1652);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.grpContainer);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ScriptPreview";
            this.Text = "Sitecore Commerce Install Assistant (SCIA) - Script Preview";
            this.panel1.ResumeLayout(false);
            this.grpContainer.ResumeLayout(false);
            this.grpContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox grpContainer;
        private System.Windows.Forms.RadioButton rbDeleteScript;
        private System.Windows.Forms.RadioButton rbUninstallScript;
        private System.Windows.Forms.RadioButton rbInstallScript;
        private System.Windows.Forms.RichTextBox lblPreview;
        private System.Windows.Forms.Label lblStatus;
    }
}