namespace SCIA
{
    partial class SiaPrerequisites
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SiaPrerequisites));
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.linkLabel6 = new System.Windows.Forms.LinkLabel();
            this.chkSitecoreSetup = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label25 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Location = new System.Drawing.Point(14, 201);
            this.panel2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1130, 216);
            this.panel2.TabIndex = 69;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel3.Controls.Add(this.linkLabel6);
            this.panel3.Controls.Add(this.chkSitecoreSetup);
            this.panel3.Location = new System.Drawing.Point(9, 28);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1048, 145);
            this.panel3.TabIndex = 0;
            // 
            // linkLabel6
            // 
            this.linkLabel6.AutoSize = true;
            this.linkLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel6.Location = new System.Drawing.Point(191, 11);
            this.linkLabel6.Name = "linkLabel6";
            this.linkLabel6.Size = new System.Drawing.Size(647, 31);
            this.linkLabel6.TabIndex = 66;
            this.linkLabel6.TabStop = true;
            this.linkLabel6.Text = "Automatically Download and Install All Pre-requisites";
            this.linkLabel6.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel6_LinkClicked);
            // 
            // chkSitecoreSetup
            // 
            this.chkSitecoreSetup.AutoSize = true;
            this.chkSitecoreSetup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkSitecoreSetup.Location = new System.Drawing.Point(96, 92);
            this.chkSitecoreSetup.Margin = new System.Windows.Forms.Padding(4);
            this.chkSitecoreSetup.Name = "chkSitecoreSetup";
            this.chkSitecoreSetup.Size = new System.Drawing.Size(856, 29);
            this.chkSitecoreSetup.TabIndex = 65;
            this.chkSitecoreSetup.Text = "Sitecore 10.0.0 rev. 004346 (Setup XP0 Developer Workstation rev. 1.2.0-r64) Fold" +
    "er";
            this.chkSitecoreSetup.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1.Location = new System.Drawing.Point(13, 13);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(158, 154);
            this.panel1.TabIndex = 71;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label25.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label25.Location = new System.Drawing.Point(194, 13);
            this.label25.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(950, 154);
            this.label25.TabIndex = 70;
            this.label25.Text = "Sitecore Install Assistant (SIA) Prerequisites";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblStatus.Location = new System.Drawing.Point(19, 462);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(1125, 32);
            this.lblStatus.TabIndex = 72;
            this.lblStatus.Text = "Happy Sitecoring!";
            // 
            // SiaPrerequisites
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1160, 503);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SiaPrerequisites";
            this.Text = "Sitecore Install Assistant Prerequisites";
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.LinkLabel linkLabel6;
        private System.Windows.Forms.CheckBox chkSitecoreSetup;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label lblStatus;
    }
}