﻿namespace SCIA
{
    partial class SitecoreContainerInstaller
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SitecoreContainerInstaller));
            this.button1 = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnValidateAll = new System.Windows.Forms.Button();
            this.btnDbConn = new System.Windows.Forms.Button();
            this.btnPrerequisites = new System.Windows.Forms.Button();
            this.btnUninstall = new System.Windows.Forms.Button();
            this.btnInstall = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.label25 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlDetails = new System.Windows.Forms.Panel();
            this.lblStepStatus = new System.Windows.Forms.Label();
            this.tabDetails = new System.Windows.Forms.TabControl();
            this.tabPgDBConnection = new System.Windows.Forms.TabPage();
            this.txtSqlPass = new System.Windows.Forms.TextBox();
            this.label49 = new System.Windows.Forms.Label();
            this.txtSqlUser = new System.Windows.Forms.TextBox();
            this.label48 = new System.Windows.Forms.Label();
            this.txtSqlDbServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabpgSiteInfo = new System.Windows.Forms.TabPage();
            this.txtIDServerSiteName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.txtSiteNameSuffix = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtSiteNamePrefix = new System.Windows.Forms.TextBox();
            this.txtSiteName = new System.Windows.Forms.TextBox();
            this.tabPgSitecore = new System.Windows.Forms.TabPage();
            this.label16 = new System.Windows.Forms.Label();
            this.txtSitecoreUserPassword = new System.Windows.Forms.TextBox();
            this.txtSitecoreUsername = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tabPgPorts = new System.Windows.Forms.TabPage();
            this.txtsaPassword = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTraefikPort2 = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.txtxConnectPort = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMsSqlPort = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSolrPort = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTraefikPort1 = new System.Windows.Forms.NumericUpDown();
            this.label34 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkStepsList = new System.Windows.Forms.CheckedListBox();
            this.lblStepInfo = new System.Windows.Forms.Label();
            this.btnAppSettings = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.toolTipSitecoreContainer = new System.Windows.Forms.ToolTip(this.components);
            this.btnPrune = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnPortCheck = new System.Windows.Forms.Button();
            this.pnlDetails.SuspendLayout();
            this.tabDetails.SuspendLayout();
            this.tabPgDBConnection.SuspendLayout();
            this.tabpgSiteInfo.SuspendLayout();
            this.tabPgSitecore.SuspendLayout();
            this.tabPgPorts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTraefikPort2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtxConnectPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMsSqlPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSolrPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTraefikPort1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(222, 82);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(36, 29);
            this.button1.TabIndex = 69;
            this.toolTipSitecoreContainer.SetToolTip(this.button1, "Cleanup xp0 Subfolders");
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblStatus.Location = new System.Drawing.Point(14, 393);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(782, 20);
            this.lblStatus.TabIndex = 67;
            this.lblStatus.Text = "Happy Sitecoring!";
            // 
            // btnValidateAll
            // 
            this.btnValidateAll.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnValidateAll.BackgroundImage")));
            this.btnValidateAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnValidateAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnValidateAll.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnValidateAll.Location = new System.Drawing.Point(115, 82);
            this.btnValidateAll.Margin = new System.Windows.Forms.Padding(2);
            this.btnValidateAll.Name = "btnValidateAll";
            this.btnValidateAll.Size = new System.Drawing.Size(36, 29);
            this.btnValidateAll.TabIndex = 62;
            this.toolTipSitecoreContainer.SetToolTip(this.btnValidateAll, "Validate All Fields....");
            this.btnValidateAll.UseVisualStyleBackColor = true;
            this.btnValidateAll.Click += new System.EventHandler(this.btnValidateAll_Click);
            // 
            // btnDbConn
            // 
            this.btnDbConn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDbConn.BackgroundImage")));
            this.btnDbConn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDbConn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDbConn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDbConn.Location = new System.Drawing.Point(80, 82);
            this.btnDbConn.Margin = new System.Windows.Forms.Padding(2);
            this.btnDbConn.Name = "btnDbConn";
            this.btnDbConn.Size = new System.Drawing.Size(36, 29);
            this.btnDbConn.TabIndex = 63;
            this.toolTipSitecoreContainer.SetToolTip(this.btnDbConn, "DB Connectivity Check");
            this.btnDbConn.UseVisualStyleBackColor = true;
            this.btnDbConn.Click += new System.EventHandler(this.btnDbConn_Click);
            // 
            // btnPrerequisites
            // 
            this.btnPrerequisites.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrerequisites.BackgroundImage")));
            this.btnPrerequisites.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPrerequisites.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrerequisites.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrerequisites.Location = new System.Drawing.Point(47, 82);
            this.btnPrerequisites.Margin = new System.Windows.Forms.Padding(2);
            this.btnPrerequisites.Name = "btnPrerequisites";
            this.btnPrerequisites.Size = new System.Drawing.Size(36, 29);
            this.btnPrerequisites.TabIndex = 64;
            this.toolTipSitecoreContainer.SetToolTip(this.btnPrerequisites, "Check Prerequisites");
            this.btnPrerequisites.UseVisualStyleBackColor = true;
            this.btnPrerequisites.Click += new System.EventHandler(this.btnPrerequisites_Click);
            // 
            // btnUninstall
            // 
            this.btnUninstall.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnUninstall.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUninstall.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUninstall.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnUninstall.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnUninstall.Location = new System.Drawing.Point(694, 366);
            this.btnUninstall.Margin = new System.Windows.Forms.Padding(2);
            this.btnUninstall.Name = "btnUninstall";
            this.btnUninstall.Size = new System.Drawing.Size(96, 25);
            this.btnUninstall.TabIndex = 65;
            this.btnUninstall.Text = "Docker Down";
            this.btnUninstall.UseVisualStyleBackColor = false;
            this.btnUninstall.EnabledChanged += new System.EventHandler(this.btnUninstall_EnabledChanged);
            this.btnUninstall.Click += new System.EventHandler(this.btnUninstall_Click);
            // 
            // btnInstall
            // 
            this.btnInstall.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnInstall.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInstall.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnInstall.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnInstall.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnInstall.Location = new System.Drawing.Point(598, 366);
            this.btnInstall.Margin = new System.Windows.Forms.Padding(2);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(90, 25);
            this.btnInstall.TabIndex = 61;
            this.btnInstall.Text = "Docker Up";
            this.btnInstall.UseVisualStyleBackColor = false;
            this.btnInstall.EnabledChanged += new System.EventHandler(this.btnInstall_EnabledChanged);
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnGenerate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGenerate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnGenerate.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnGenerate.Location = new System.Drawing.Point(461, 366);
            this.btnGenerate.Margin = new System.Windows.Forms.Padding(2);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(132, 25);
            this.btnGenerate.TabIndex = 60;
            this.btnGenerate.Text = "Generate .env file";
            this.btnGenerate.UseVisualStyleBackColor = false;
            this.btnGenerate.EnabledChanged += new System.EventHandler(this.btnGenerate_EnabledChanged);
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label25.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label25.Location = new System.Drawing.Point(94, 12);
            this.label25.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(701, 60);
            this.label25.TabIndex = 59;
            this.label25.Text = "Sitecore Container Installer";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1.Location = new System.Drawing.Point(11, 10);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(79, 62);
            this.panel1.TabIndex = 58;
            // 
            // pnlDetails
            // 
            this.pnlDetails.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlDetails.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlDetails.Controls.Add(this.lblStepStatus);
            this.pnlDetails.Controls.Add(this.tabDetails);
            this.pnlDetails.Location = new System.Drawing.Point(12, 120);
            this.pnlDetails.Margin = new System.Windows.Forms.Padding(2);
            this.pnlDetails.Name = "pnlDetails";
            this.pnlDetails.Size = new System.Drawing.Size(608, 221);
            this.pnlDetails.TabIndex = 66;
            // 
            // lblStepStatus
            // 
            this.lblStepStatus.AutoSize = true;
            this.lblStepStatus.Location = new System.Drawing.Point(518, -51);
            this.lblStepStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStepStatus.Name = "lblStepStatus";
            this.lblStepStatus.Size = new System.Drawing.Size(62, 13);
            this.lblStepStatus.TabIndex = 47;
            this.lblStepStatus.Text = "Step Status";
            // 
            // tabDetails
            // 
            this.tabDetails.Controls.Add(this.tabPgDBConnection);
            this.tabDetails.Controls.Add(this.tabpgSiteInfo);
            this.tabDetails.Controls.Add(this.tabPgSitecore);
            this.tabDetails.Controls.Add(this.tabPgPorts);
            this.tabDetails.Location = new System.Drawing.Point(3, -1);
            this.tabDetails.Margin = new System.Windows.Forms.Padding(2);
            this.tabDetails.Multiline = true;
            this.tabDetails.Name = "tabDetails";
            this.tabDetails.SelectedIndex = 0;
            this.tabDetails.Size = new System.Drawing.Size(587, 206);
            this.tabDetails.TabIndex = 2;
            // 
            // tabPgDBConnection
            // 
            this.tabPgDBConnection.Controls.Add(this.txtSqlPass);
            this.tabPgDBConnection.Controls.Add(this.label49);
            this.tabPgDBConnection.Controls.Add(this.txtSqlUser);
            this.tabPgDBConnection.Controls.Add(this.label48);
            this.tabPgDBConnection.Controls.Add(this.txtSqlDbServer);
            this.tabPgDBConnection.Controls.Add(this.label1);
            this.tabPgDBConnection.Location = new System.Drawing.Point(4, 22);
            this.tabPgDBConnection.Margin = new System.Windows.Forms.Padding(2);
            this.tabPgDBConnection.Name = "tabPgDBConnection";
            this.tabPgDBConnection.Size = new System.Drawing.Size(579, 180);
            this.tabPgDBConnection.TabIndex = 13;
            this.tabPgDBConnection.Text = "DB Connection";
            // 
            // txtSqlPass
            // 
            this.txtSqlPass.Enabled = false;
            this.txtSqlPass.Location = new System.Drawing.Point(134, 54);
            this.txtSqlPass.Margin = new System.Windows.Forms.Padding(2);
            this.txtSqlPass.MaxLength = 15;
            this.txtSqlPass.Name = "txtSqlPass";
            this.txtSqlPass.Size = new System.Drawing.Size(435, 20);
            this.txtSqlPass.TabIndex = 50;
            this.txtSqlPass.UseSystemPasswordChar = true;
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(5, 56);
            this.label49.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(48, 13);
            this.label49.TabIndex = 0;
            this.label49.Text = "SqlPass:";
            // 
            // txtSqlUser
            // 
            this.txtSqlUser.Enabled = false;
            this.txtSqlUser.Location = new System.Drawing.Point(134, 32);
            this.txtSqlUser.Margin = new System.Windows.Forms.Padding(2);
            this.txtSqlUser.MaxLength = 20;
            this.txtSqlUser.Name = "txtSqlUser";
            this.txtSqlUser.Size = new System.Drawing.Size(435, 20);
            this.txtSqlUser.TabIndex = 14;
            this.txtSqlUser.Text = "sa";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(5, 34);
            this.label48.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(47, 13);
            this.label48.TabIndex = 0;
            this.label48.Text = "SqlUser:";
            // 
            // txtSqlDbServer
            // 
            this.txtSqlDbServer.Enabled = false;
            this.txtSqlDbServer.Location = new System.Drawing.Point(134, 11);
            this.txtSqlDbServer.Margin = new System.Windows.Forms.Padding(2);
            this.txtSqlDbServer.MaxLength = 25;
            this.txtSqlDbServer.Name = "txtSqlDbServer";
            this.txtSqlDbServer.Size = new System.Drawing.Size(435, 20);
            this.txtSqlDbServer.TabIndex = 12;
            this.txtSqlDbServer.Text = "(local)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "DbServer:";
            // 
            // tabpgSiteInfo
            // 
            this.tabpgSiteInfo.Controls.Add(this.txtIDServerSiteName);
            this.tabpgSiteInfo.Controls.Add(this.label2);
            this.tabpgSiteInfo.Controls.Add(this.label38);
            this.tabpgSiteInfo.Controls.Add(this.label26);
            this.tabpgSiteInfo.Controls.Add(this.txtSiteNameSuffix);
            this.tabpgSiteInfo.Controls.Add(this.label22);
            this.tabpgSiteInfo.Controls.Add(this.txtSiteNamePrefix);
            this.tabpgSiteInfo.Controls.Add(this.txtSiteName);
            this.tabpgSiteInfo.Location = new System.Drawing.Point(4, 22);
            this.tabpgSiteInfo.Margin = new System.Windows.Forms.Padding(2);
            this.tabpgSiteInfo.Name = "tabpgSiteInfo";
            this.tabpgSiteInfo.Size = new System.Drawing.Size(579, 180);
            this.tabpgSiteInfo.TabIndex = 12;
            this.tabpgSiteInfo.Text = "Site Info";
            // 
            // txtIDServerSiteName
            // 
            this.txtIDServerSiteName.Location = new System.Drawing.Point(133, 70);
            this.txtIDServerSiteName.Margin = new System.Windows.Forms.Padding(2);
            this.txtIDServerSiteName.MaxLength = 50;
            this.txtIDServerSiteName.Name = "txtIDServerSiteName";
            this.txtIDServerSiteName.Size = new System.Drawing.Size(440, 20);
            this.txtIDServerSiteName.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 71);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "IDServerSiteName:";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(2, 11);
            this.label38.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(82, 13);
            this.label38.TabIndex = 0;
            this.label38.Text = "SiteNamePrefix:";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(2, 53);
            this.label26.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(56, 13);
            this.label26.TabIndex = 7;
            this.label26.Text = "SiteName:";
            // 
            // txtSiteNameSuffix
            // 
            this.txtSiteNameSuffix.Location = new System.Drawing.Point(132, 29);
            this.txtSiteNameSuffix.Margin = new System.Windows.Forms.Padding(2);
            this.txtSiteNameSuffix.MaxLength = 50;
            this.txtSiteNameSuffix.Name = "txtSiteNameSuffix";
            this.txtSiteNameSuffix.Size = new System.Drawing.Size(441, 20);
            this.txtSiteNameSuffix.TabIndex = 1;
            this.txtSiteNameSuffix.TextChanged += new System.EventHandler(this.txtSiteNameSuffix_TextChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(2, 31);
            this.label22.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(82, 13);
            this.label22.TabIndex = 0;
            this.label22.Text = "SiteNameSuffix:";
            // 
            // txtSiteNamePrefix
            // 
            this.txtSiteNamePrefix.Location = new System.Drawing.Point(132, 9);
            this.txtSiteNamePrefix.Margin = new System.Windows.Forms.Padding(2);
            this.txtSiteNamePrefix.MaxLength = 50;
            this.txtSiteNamePrefix.Name = "txtSiteNamePrefix";
            this.txtSiteNamePrefix.Size = new System.Drawing.Size(441, 20);
            this.txtSiteNamePrefix.TabIndex = 1;
            this.txtSiteNamePrefix.TextChanged += new System.EventHandler(this.txtSiteNamePrefix_TextChanged);
            // 
            // txtSiteName
            // 
            this.txtSiteName.Location = new System.Drawing.Point(132, 49);
            this.txtSiteName.Margin = new System.Windows.Forms.Padding(2);
            this.txtSiteName.MaxLength = 100;
            this.txtSiteName.Name = "txtSiteName";
            this.txtSiteName.Size = new System.Drawing.Size(441, 20);
            this.txtSiteName.TabIndex = 2;
            this.txtSiteName.Text = "scom10sxa.dev.local";
            // 
            // tabPgSitecore
            // 
            this.tabPgSitecore.Controls.Add(this.label16);
            this.tabPgSitecore.Controls.Add(this.txtSitecoreUserPassword);
            this.tabPgSitecore.Controls.Add(this.txtSitecoreUsername);
            this.tabPgSitecore.Controls.Add(this.label15);
            this.tabPgSitecore.Location = new System.Drawing.Point(4, 22);
            this.tabPgSitecore.Margin = new System.Windows.Forms.Padding(2);
            this.tabPgSitecore.Name = "tabPgSitecore";
            this.tabPgSitecore.Size = new System.Drawing.Size(579, 180);
            this.tabPgSitecore.TabIndex = 4;
            this.tabPgSitecore.Text = "Sitecore";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(3, 35);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(117, 13);
            this.label16.TabIndex = 0;
            this.label16.Text = "SitecoreUserPassword:";
            // 
            // txtSitecoreUserPassword
            // 
            this.txtSitecoreUserPassword.Location = new System.Drawing.Point(132, 33);
            this.txtSitecoreUserPassword.Margin = new System.Windows.Forms.Padding(2);
            this.txtSitecoreUserPassword.MaxLength = 15;
            this.txtSitecoreUserPassword.Name = "txtSitecoreUserPassword";
            this.txtSitecoreUserPassword.Size = new System.Drawing.Size(438, 20);
            this.txtSitecoreUserPassword.TabIndex = 18;
            this.txtSitecoreUserPassword.Text = "b";
            this.txtSitecoreUserPassword.UseSystemPasswordChar = true;
            // 
            // txtSitecoreUsername
            // 
            this.txtSitecoreUsername.Enabled = false;
            this.txtSitecoreUsername.Location = new System.Drawing.Point(132, 11);
            this.txtSitecoreUsername.Margin = new System.Windows.Forms.Padding(2);
            this.txtSitecoreUsername.MaxLength = 15;
            this.txtSitecoreUsername.Name = "txtSitecoreUsername";
            this.txtSitecoreUsername.Size = new System.Drawing.Size(438, 20);
            this.txtSitecoreUsername.TabIndex = 17;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(3, 13);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(99, 13);
            this.label15.TabIndex = 0;
            this.label15.Text = "SitecoreUserName:";
            // 
            // tabPgPorts
            // 
            this.tabPgPorts.Controls.Add(this.txtsaPassword);
            this.tabPgPorts.Controls.Add(this.label7);
            this.tabPgPorts.Controls.Add(this.txtTraefikPort2);
            this.tabPgPorts.Controls.Add(this.label6);
            this.tabPgPorts.Controls.Add(this.txtxConnectPort);
            this.tabPgPorts.Controls.Add(this.label5);
            this.tabPgPorts.Controls.Add(this.txtMsSqlPort);
            this.tabPgPorts.Controls.Add(this.label4);
            this.tabPgPorts.Controls.Add(this.txtSolrPort);
            this.tabPgPorts.Controls.Add(this.label3);
            this.tabPgPorts.Controls.Add(this.txtTraefikPort1);
            this.tabPgPorts.Controls.Add(this.label34);
            this.tabPgPorts.Location = new System.Drawing.Point(4, 22);
            this.tabPgPorts.Margin = new System.Windows.Forms.Padding(2);
            this.tabPgPorts.Name = "tabPgPorts";
            this.tabPgPorts.Size = new System.Drawing.Size(579, 180);
            this.tabPgPorts.TabIndex = 14;
            this.tabPgPorts.Text = "Ports";
            this.tabPgPorts.UseVisualStyleBackColor = true;
            // 
            // txtsaPassword
            // 
            this.txtsaPassword.Location = new System.Drawing.Point(156, 115);
            this.txtsaPassword.Margin = new System.Windows.Forms.Padding(2);
            this.txtsaPassword.MaxLength = 15;
            this.txtsaPassword.Name = "txtsaPassword";
            this.txtsaPassword.Size = new System.Drawing.Size(418, 20);
            this.txtsaPassword.TabIndex = 43;
            this.txtsaPassword.Text = "P@ssword12345";
            this.toolTipSitecoreContainer.SetToolTip(this.txtsaPassword, "Used to login to SQL Server using the MSSqlPort provided above");
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(12, 115);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(116, 36);
            this.label7.TabIndex = 42;
            this.label7.Text = "SqlPass (different from default sa password):";
            // 
            // txtTraefikPort2
            // 
            this.txtTraefikPort2.Location = new System.Drawing.Point(156, 33);
            this.txtTraefikPort2.Margin = new System.Windows.Forms.Padding(2);
            this.txtTraefikPort2.Maximum = new decimal(new int[] {
            49000,
            0,
            0,
            0});
            this.txtTraefikPort2.Name = "txtTraefikPort2";
            this.txtTraefikPort2.Size = new System.Drawing.Size(112, 20);
            this.txtTraefikPort2.TabIndex = 41;
            this.txtTraefikPort2.Value = new decimal(new int[] {
            8079,
            0,
            0,
            0});
            this.txtTraefikPort2.ValueChanged += new System.EventHandler(this.numericUpDown3_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 35);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 40;
            this.label6.Text = "TraefikPort2:";
            // 
            // txtxConnectPort
            // 
            this.txtxConnectPort.Location = new System.Drawing.Point(156, 93);
            this.txtxConnectPort.Margin = new System.Windows.Forms.Padding(2);
            this.txtxConnectPort.Maximum = new decimal(new int[] {
            49000,
            0,
            0,
            0});
            this.txtxConnectPort.Name = "txtxConnectPort";
            this.txtxConnectPort.Size = new System.Drawing.Size(112, 20);
            this.txtxConnectPort.TabIndex = 39;
            this.txtxConnectPort.Value = new decimal(new int[] {
            8081,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 96);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 38;
            this.label5.Text = "xConnectPort:";
            // 
            // txtMsSqlPort
            // 
            this.txtMsSqlPort.Location = new System.Drawing.Point(156, 73);
            this.txtMsSqlPort.Margin = new System.Windows.Forms.Padding(2);
            this.txtMsSqlPort.Maximum = new decimal(new int[] {
            49000,
            0,
            0,
            0});
            this.txtMsSqlPort.Name = "txtMsSqlPort";
            this.txtMsSqlPort.Size = new System.Drawing.Size(112, 20);
            this.txtMsSqlPort.TabIndex = 37;
            this.txtMsSqlPort.Value = new decimal(new int[] {
            14330,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 75);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 36;
            this.label4.Text = "MSSqlPort:";
            // 
            // txtSolrPort
            // 
            this.txtSolrPort.Location = new System.Drawing.Point(156, 53);
            this.txtSolrPort.Margin = new System.Windows.Forms.Padding(2);
            this.txtSolrPort.Maximum = new decimal(new int[] {
            49000,
            0,
            0,
            0});
            this.txtSolrPort.Name = "txtSolrPort";
            this.txtSolrPort.Size = new System.Drawing.Size(112, 20);
            this.txtSolrPort.TabIndex = 35;
            this.txtSolrPort.Value = new decimal(new int[] {
            8984,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 55);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 34;
            this.label3.Text = "SolrPort:";
            // 
            // txtTraefikPort1
            // 
            this.txtTraefikPort1.Enabled = false;
            this.txtTraefikPort1.Location = new System.Drawing.Point(156, 12);
            this.txtTraefikPort1.Margin = new System.Windows.Forms.Padding(2);
            this.txtTraefikPort1.Maximum = new decimal(new int[] {
            49000,
            0,
            0,
            0});
            this.txtTraefikPort1.Name = "txtTraefikPort1";
            this.txtTraefikPort1.Size = new System.Drawing.Size(112, 20);
            this.txtTraefikPort1.TabIndex = 33;
            this.txtTraefikPort1.Value = new decimal(new int[] {
            443,
            0,
            0,
            0});
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(12, 15);
            this.label34.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(68, 13);
            this.label34.TabIndex = 32;
            this.label34.Text = "TraefikPort1:";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.chkStepsList);
            this.panel2.Location = new System.Drawing.Point(622, 120);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(174, 221);
            this.panel2.TabIndex = 71;
            // 
            // chkStepsList
            // 
            this.chkStepsList.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.chkStepsList.CheckOnClick = true;
            this.chkStepsList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkStepsList.FormattingEnabled = true;
            this.chkStepsList.Items.AddRange(new object[] {
            "DB Connection",
            "Site Info",
            "Sitecore ",
            "Ports"});
            this.chkStepsList.Location = new System.Drawing.Point(2, 2);
            this.chkStepsList.Margin = new System.Windows.Forms.Padding(2);
            this.chkStepsList.Name = "chkStepsList";
            this.chkStepsList.Size = new System.Drawing.Size(147, 199);
            this.chkStepsList.TabIndex = 53;
            this.chkStepsList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkStepsList_ItemCheck);
            this.chkStepsList.Click += new System.EventHandler(this.chkStepsList_Click);
            // 
            // lblStepInfo
            // 
            this.lblStepInfo.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.lblStepInfo.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblStepInfo.Location = new System.Drawing.Point(590, 90);
            this.lblStepInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStepInfo.Name = "lblStepInfo";
            this.lblStepInfo.Size = new System.Drawing.Size(204, 21);
            this.lblStepInfo.TabIndex = 70;
            this.lblStepInfo.Text = "Step 1 of 6: DB Connection";
            this.lblStepInfo.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnAppSettings
            // 
            this.btnAppSettings.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAppSettings.BackgroundImage")));
            this.btnAppSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAppSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAppSettings.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAppSettings.Location = new System.Drawing.Point(11, 82);
            this.btnAppSettings.Margin = new System.Windows.Forms.Padding(2);
            this.btnAppSettings.Name = "btnAppSettings";
            this.btnAppSettings.Size = new System.Drawing.Size(35, 29);
            this.btnAppSettings.TabIndex = 72;
            this.btnAppSettings.UseVisualStyleBackColor = true;
            // 
            // btnPrevious
            // 
            this.btnPrevious.BackColor = System.Drawing.Color.White;
            this.btnPrevious.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrevious.BackgroundImage")));
            this.btnPrevious.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPrevious.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrevious.Location = new System.Drawing.Point(362, 82);
            this.btnPrevious.Margin = new System.Windows.Forms.Padding(2);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(33, 29);
            this.btnPrevious.TabIndex = 73;
            this.toolTipSitecoreContainer.SetToolTip(this.btnPrevious, "Go to Previous Tab");
            this.btnPrevious.UseVisualStyleBackColor = false;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.BackColor = System.Drawing.Color.White;
            this.btnFirst.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnFirst.BackgroundImage")));
            this.btnFirst.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnFirst.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFirst.Location = new System.Drawing.Point(330, 82);
            this.btnFirst.Margin = new System.Windows.Forms.Padding(2);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(33, 29);
            this.btnFirst.TabIndex = 74;
            this.toolTipSitecoreContainer.SetToolTip(this.btnFirst, "Go to First Tab");
            this.btnFirst.UseVisualStyleBackColor = false;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnLast
            // 
            this.btnLast.BackColor = System.Drawing.Color.White;
            this.btnLast.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLast.BackgroundImage")));
            this.btnLast.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnLast.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLast.Location = new System.Drawing.Point(428, 82);
            this.btnLast.Margin = new System.Windows.Forms.Padding(2);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(33, 29);
            this.btnLast.TabIndex = 75;
            this.toolTipSitecoreContainer.SetToolTip(this.btnLast, "Go to Last Tab");
            this.btnLast.UseVisualStyleBackColor = false;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.White;
            this.btnNext.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnNext.BackgroundImage")));
            this.btnNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNext.Location = new System.Drawing.Point(396, 82);
            this.btnNext.Margin = new System.Windows.Forms.Padding(2);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(33, 29);
            this.btnNext.TabIndex = 76;
            this.toolTipSitecoreContainer.SetToolTip(this.btnNext, "Go to Next Tab");
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // button2
            // 
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Location = new System.Drawing.Point(186, 82);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(36, 29);
            this.button2.TabIndex = 77;
            this.toolTipSitecoreContainer.SetToolTip(this.button2, "List Docker Containers");
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnPrune
            // 
            this.btnPrune.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrune.BackgroundImage")));
            this.btnPrune.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPrune.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrune.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrune.Location = new System.Drawing.Point(258, 82);
            this.btnPrune.Margin = new System.Windows.Forms.Padding(2);
            this.btnPrune.Name = "btnPrune";
            this.btnPrune.Size = new System.Drawing.Size(36, 29);
            this.btnPrune.TabIndex = 79;
            this.toolTipSitecoreContainer.SetToolTip(this.btnPrune, "Docker Prune");
            this.btnPrune.UseVisualStyleBackColor = true;
            this.btnPrune.Click += new System.EventHandler(this.btnPrune_Click);
            // 
            // button3
            // 
            this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Location = new System.Drawing.Point(294, 82);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(36, 29);
            this.button3.TabIndex = 80;
            this.toolTipSitecoreContainer.SetToolTip(this.button3, "Docker Kill");
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnPortCheck
            // 
            this.btnPortCheck.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPortCheck.BackgroundImage")));
            this.btnPortCheck.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPortCheck.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPortCheck.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPortCheck.Location = new System.Drawing.Point(150, 82);
            this.btnPortCheck.Margin = new System.Windows.Forms.Padding(2);
            this.btnPortCheck.Name = "btnPortCheck";
            this.btnPortCheck.Size = new System.Drawing.Size(35, 29);
            this.btnPortCheck.TabIndex = 78;
            this.btnPortCheck.UseVisualStyleBackColor = true;
            this.btnPortCheck.Click += new System.EventHandler(this.btnPortCheck_Click);
            // 
            // SitecoreContainerInstaller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 419);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnPrune);
            this.Controls.Add(this.btnPortCheck);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnFirst);
            this.Controls.Add(this.btnLast);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnAppSettings);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lblStepInfo);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnValidateAll);
            this.Controls.Add(this.btnDbConn);
            this.Controls.Add(this.btnPrerequisites);
            this.Controls.Add(this.btnUninstall);
            this.Controls.Add(this.btnInstall);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlDetails);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "SitecoreContainerInstaller";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SCIA - Sitecore Container Installer";
            this.Load += new System.EventHandler(this.SitecoreContainerInstaller_Load);
            this.pnlDetails.ResumeLayout(false);
            this.pnlDetails.PerformLayout();
            this.tabDetails.ResumeLayout(false);
            this.tabPgDBConnection.ResumeLayout(false);
            this.tabPgDBConnection.PerformLayout();
            this.tabpgSiteInfo.ResumeLayout(false);
            this.tabpgSiteInfo.PerformLayout();
            this.tabPgSitecore.ResumeLayout(false);
            this.tabPgSitecore.PerformLayout();
            this.tabPgPorts.ResumeLayout(false);
            this.tabPgPorts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTraefikPort2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtxConnectPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMsSqlPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSolrPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTraefikPort1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnValidateAll;
        private System.Windows.Forms.Button btnDbConn;
        private System.Windows.Forms.Button btnPrerequisites;
        private System.Windows.Forms.Button btnUninstall;
        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlDetails;
        private System.Windows.Forms.Label lblStepStatus;
        private System.Windows.Forms.TabControl tabDetails;
        private System.Windows.Forms.TabPage tabPgDBConnection;
        private System.Windows.Forms.TextBox txtSqlPass;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.TextBox txtSqlUser;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.TextBox txtSqlDbServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabpgSiteInfo;
        private System.Windows.Forms.TextBox txtIDServerSiteName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox txtSiteNameSuffix;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtSiteNamePrefix;
        private System.Windows.Forms.TextBox txtSiteName;
        private System.Windows.Forms.TabPage tabPgSitecore;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtSitecoreUserPassword;
        private System.Windows.Forms.TextBox txtSitecoreUsername;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckedListBox chkStepsList;
        private System.Windows.Forms.Label lblStepInfo;
        private System.Windows.Forms.Button btnAppSettings;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolTip toolTipSitecoreContainer;
        private System.Windows.Forms.TabPage tabPgPorts;
        private System.Windows.Forms.NumericUpDown txtTraefikPort2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown txtxConnectPort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown txtMsSqlPort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown txtSolrPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown txtTraefikPort1;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Button btnPortCheck;
        private System.Windows.Forms.TextBox txtsaPassword;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnPrune;
        private System.Windows.Forms.Button button3;
    }
}