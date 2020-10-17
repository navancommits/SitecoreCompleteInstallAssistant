namespace SCIA
{
    partial class PortCheck
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PortCheck));
            this.chkCommerceOpsPort = new System.Windows.Forms.CheckBox();
            this.chkCommerceShopsPort = new System.Windows.Forms.CheckBox();
            this.chkCommerceAuhPort = new System.Windows.Forms.CheckBox();
            this.chkCommerceMinionsPort = new System.Windows.Forms.CheckBox();
            this.chkBizFxPort = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // chkCommerceOpsPort
            // 
            this.chkCommerceOpsPort.AutoSize = true;
            this.chkCommerceOpsPort.Location = new System.Drawing.Point(21, 139);
            this.chkCommerceOpsPort.Name = "chkCommerceOpsPort";
            this.chkCommerceOpsPort.Size = new System.Drawing.Size(214, 24);
            this.chkCommerceOpsPort.TabIndex = 0;
            this.chkCommerceOpsPort.Text = "Commerce Ops Service Port";
            this.chkCommerceOpsPort.UseVisualStyleBackColor = true;
            // 
            // chkCommerceShopsPort
            // 
            this.chkCommerceShopsPort.AutoSize = true;
            this.chkCommerceShopsPort.Location = new System.Drawing.Point(21, 169);
            this.chkCommerceShopsPort.Name = "chkCommerceShopsPort";
            this.chkCommerceShopsPort.Size = new System.Drawing.Size(228, 24);
            this.chkCommerceShopsPort.TabIndex = 0;
            this.chkCommerceShopsPort.Text = "Commerce Shops Service Port";
            this.chkCommerceShopsPort.UseVisualStyleBackColor = true;
            // 
            // chkCommerceAuhPort
            // 
            this.chkCommerceAuhPort.AutoSize = true;
            this.chkCommerceAuhPort.Location = new System.Drawing.Point(21, 199);
            this.chkCommerceAuhPort.Name = "chkCommerceAuhPort";
            this.chkCommerceAuhPort.Size = new System.Drawing.Size(254, 24);
            this.chkCommerceAuhPort.TabIndex = 0;
            this.chkCommerceAuhPort.Text = "Commerce Authoring Service Port";
            this.chkCommerceAuhPort.UseVisualStyleBackColor = true;
            // 
            // chkCommerceMinionsPort
            // 
            this.chkCommerceMinionsPort.AutoSize = true;
            this.chkCommerceMinionsPort.Location = new System.Drawing.Point(21, 229);
            this.chkCommerceMinionsPort.Name = "chkCommerceMinionsPort";
            this.chkCommerceMinionsPort.Size = new System.Drawing.Size(240, 24);
            this.chkCommerceMinionsPort.TabIndex = 0;
            this.chkCommerceMinionsPort.Text = "Commerce Minions Service Port";
            this.chkCommerceMinionsPort.UseVisualStyleBackColor = true;
            // 
            // chkBizFxPort
            // 
            this.chkBizFxPort.AutoSize = true;
            this.chkBizFxPort.Location = new System.Drawing.Point(21, 259);
            this.chkBizFxPort.Name = "chkBizFxPort";
            this.chkBizFxPort.Size = new System.Drawing.Size(146, 24);
            this.chkBizFxPort.TabIndex = 0;
            this.chkBizFxPort.Text = "BizFx Service Port";
            this.chkBizFxPort.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(120, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(668, 50);
            this.label1.TabIndex = 8;
            this.label1.Text = "Sitecore Commerce Install Assistant (SCIA)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(120, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(668, 46);
            this.label2.TabIndex = 8;
            this.label2.Text = "Port Availability";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel1.Location = new System.Drawing.Point(9, 9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(105, 96);
            this.panel1.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel2.Location = new System.Drawing.Point(530, 124);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(258, 199);
            this.panel2.TabIndex = 5;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblStatus.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblStatus.Location = new System.Drawing.Point(21, 339);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(133, 20);
            this.lblStatus.TabIndex = 52;
            this.lblStatus.Text = "Happy Sitecoring!";
            // 
            // PortCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 368);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkBizFxPort);
            this.Controls.Add(this.chkCommerceMinionsPort);
            this.Controls.Add(this.chkCommerceAuhPort);
            this.Controls.Add(this.chkCommerceShopsPort);
            this.Controls.Add(this.chkCommerceOpsPort);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PortCheck";
            this.Text = "Port Availability";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkCommerceOpsPort;
        private System.Windows.Forms.CheckBox chkCommerceShopsPort;
        private System.Windows.Forms.CheckBox chkCommerceAuhPort;
        private System.Windows.Forms.CheckBox chkCommerceMinionsPort;
        private System.Windows.Forms.CheckBox chkBizFxPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblStatus;
    }
}