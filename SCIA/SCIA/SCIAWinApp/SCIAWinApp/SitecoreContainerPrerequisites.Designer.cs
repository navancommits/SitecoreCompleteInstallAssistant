﻿namespace SCIA
{
    partial class SitecoreContainerPrerequisites
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SitecoreContainerPrerequisites));
            this.label25 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkLicenseFile = new System.Windows.Forms.CheckBox();
            this.chkWindowsEdition = new System.Windows.Forms.CheckBox();
            this.linkLabel6 = new System.Windows.Forms.LinkLabel();
            this.chkDocker = new System.Windows.Forms.CheckBox();
            this.chkSitecoreContainer = new System.Windows.Forms.CheckBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            this.pnlContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label25.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label25.Location = new System.Drawing.Point(193, 29);
            this.label25.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(1213, 135);
            this.label25.TabIndex = 64;
            this.label25.Text = "Sitecore Container Prerequisites";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1.Location = new System.Drawing.Point(26, 29);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(142, 135);
            this.panel1.TabIndex = 63;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.chkLicenseFile);
            this.panel2.Controls.Add(this.chkWindowsEdition);
            this.panel2.Controls.Add(this.linkLabel6);
            this.panel2.Controls.Add(this.chkDocker);
            this.panel2.Controls.Add(this.chkSitecoreContainer);
            this.panel2.Location = new System.Drawing.Point(13, 32);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1301, 267);
            this.panel2.TabIndex = 2;
            // 
            // chkLicenseFile
            // 
            this.chkLicenseFile.AutoSize = true;
            this.chkLicenseFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkLicenseFile.Location = new System.Drawing.Point(648, 173);
            this.chkLicenseFile.Margin = new System.Windows.Forms.Padding(4);
            this.chkLicenseFile.Name = "chkLicenseFile";
            this.chkLicenseFile.Size = new System.Drawing.Size(160, 29);
            this.chkLicenseFile.TabIndex = 66;
            this.chkLicenseFile.Text = "License File";
            this.chkLicenseFile.UseVisualStyleBackColor = false;
            // 
            // chkWindowsEdition
            // 
            this.chkWindowsEdition.AutoSize = true;
            this.chkWindowsEdition.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkWindowsEdition.Location = new System.Drawing.Point(34, 173);
            this.chkWindowsEdition.Margin = new System.Windows.Forms.Padding(4);
            this.chkWindowsEdition.Name = "chkWindowsEdition";
            this.chkWindowsEdition.Size = new System.Drawing.Size(383, 29);
            this.chkWindowsEdition.TabIndex = 65;
            this.chkWindowsEdition.Text = "Windows 10 Pro or Enterprise Build";
            this.chkWindowsEdition.UseVisualStyleBackColor = false;
            // 
            // linkLabel6
            // 
            this.linkLabel6.AutoSize = true;
            this.linkLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel6.Location = new System.Drawing.Point(479, 35);
            this.linkLabel6.Name = "linkLabel6";
            this.linkLabel6.Size = new System.Drawing.Size(441, 31);
            this.linkLabel6.TabIndex = 64;
            this.linkLabel6.TabStop = true;
            this.linkLabel6.Text = "Download and Install Pre-requisites";
            this.linkLabel6.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel6_LinkClicked);
            // 
            // chkDocker
            // 
            this.chkDocker.AutoSize = true;
            this.chkDocker.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkDocker.Location = new System.Drawing.Point(34, 123);
            this.chkDocker.Margin = new System.Windows.Forms.Padding(4);
            this.chkDocker.Name = "chkDocker";
            this.chkDocker.Size = new System.Drawing.Size(321, 29);
            this.chkDocker.TabIndex = 0;
            this.chkDocker.Text = "Docker Desktop for Windows";
            this.chkDocker.UseVisualStyleBackColor = false;
            // 
            // chkSitecoreContainer
            // 
            this.chkSitecoreContainer.AutoSize = true;
            this.chkSitecoreContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkSitecoreContainer.Location = new System.Drawing.Point(648, 123);
            this.chkSitecoreContainer.Margin = new System.Windows.Forms.Padding(4);
            this.chkSitecoreContainer.Name = "chkSitecoreContainer";
            this.chkSitecoreContainer.Size = new System.Drawing.Size(525, 29);
            this.chkSitecoreContainer.TabIndex = 0;
            this.chkSitecoreContainer.Text = "Sitecore.Commerce.Container.SDK.1.0.214 Folder";
            this.chkSitecoreContainer.UseVisualStyleBackColor = false;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblStatus.Location = new System.Drawing.Point(31, 589);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(1379, 38);
            this.lblStatus.TabIndex = 66;
            this.lblStatus.Text = "Happy Sitecoring!";
            // 
            // pnlContainer
            // 
            this.pnlContainer.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlContainer.Controls.Add(this.panel2);
            this.pnlContainer.Location = new System.Drawing.Point(26, 198);
            this.pnlContainer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(1380, 346);
            this.pnlContainer.TabIndex = 65;
            // 
            // SitecoreContainerPrerequisites
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1445, 637);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.pnlContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SitecoreContainerPrerequisites";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SCIA - Sitecore Container Prerequisites";
            this.Load += new System.EventHandler(this.SitecoreContainerPrerequisites_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox chkLicenseFile;
        private System.Windows.Forms.CheckBox chkWindowsEdition;
        private System.Windows.Forms.LinkLabel linkLabel6;
        private System.Windows.Forms.CheckBox chkDocker;
        private System.Windows.Forms.CheckBox chkSitecoreContainer;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel pnlContainer;
    }
}