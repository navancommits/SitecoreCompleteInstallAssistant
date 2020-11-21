namespace SCIA
{
    partial class DBSetup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBSetup));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label25 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chkZipVersions = new System.Windows.Forms.CheckBox();
            this.chkVersionPrerequisites = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1.Location = new System.Drawing.Point(28, 28);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(158, 154);
            this.panel1.TabIndex = 78;
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label25.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label25.Location = new System.Drawing.Point(201, 28);
            this.label25.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(950, 154);
            this.label25.TabIndex = 77;
            this.label25.Text = "SCIA DB Tables Setup";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(34, 221);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(227, 29);
            this.label3.TabIndex = 81;
            this.label3.Text = "Use this form to - ";
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblStatus.Location = new System.Drawing.Point(28, 702);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(1122, 38);
            this.lblStatus.TabIndex = 80;
            this.lblStatus.Text = "Happy Sitecoring!";
            // 
            // btnCreate
            // 
            this.btnCreate.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnCreate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCreate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCreate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCreate.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCreate.Location = new System.Drawing.Point(971, 639);
            this.btnCreate.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(180, 48);
            this.btnCreate.TabIndex = 79;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = false;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Location = new System.Drawing.Point(28, 371);
            this.panel2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1122, 245);
            this.panel2.TabIndex = 82;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel3.Controls.Add(this.chkVersionPrerequisites);
            this.panel3.Controls.Add(this.chkZipVersions);
            this.panel3.Location = new System.Drawing.Point(9, 28);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1053, 156);
            this.panel3.TabIndex = 0;
            // 
            // chkZipVersions
            // 
            this.chkZipVersions.AutoSize = true;
            this.chkZipVersions.Checked = true;
            this.chkZipVersions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkZipVersions.Location = new System.Drawing.Point(24, 56);
            this.chkZipVersions.Name = "chkZipVersions";
            this.chkZipVersions.Size = new System.Drawing.Size(342, 29);
            this.chkZipVersions.TabIndex = 0;
            this.chkZipVersions.Text = "Create SCIA ZipVersions Table";
            this.chkZipVersions.UseVisualStyleBackColor = true;
            // 
            // chkVersionPrerequisites
            // 
            this.chkVersionPrerequisites.AutoSize = true;
            this.chkVersionPrerequisites.Checked = true;
            this.chkVersionPrerequisites.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVersionPrerequisites.Location = new System.Drawing.Point(587, 56);
            this.chkVersionPrerequisites.Name = "chkVersionPrerequisites";
            this.chkVersionPrerequisites.Size = new System.Drawing.Size(427, 29);
            this.chkVersionPrerequisites.TabIndex = 1;
            this.chkVersionPrerequisites.Text = "Create SCIA VersionPrerequisites Table";
            this.chkVersionPrerequisites.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(34, 265);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(897, 29);
            this.label1.TabIndex = 83;
            this.label1.Text = "1. Setup the SCIA ZipVersions and VersionPrerequisites DB tables for the first ti" +
    "me";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(34, 306);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(897, 29);
            this.label2.TabIndex = 84;
            this.label2.Text = "2. Delete and recreate the SCIA ZipVersions and VersionPrerequisites DB tables";
            // 
            // DBSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1179, 748);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DBSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SCIA - Setup Sitecore Versions and Prerequisites DB";
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox chkVersionPrerequisites;
        private System.Windows.Forms.CheckBox chkZipVersions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}