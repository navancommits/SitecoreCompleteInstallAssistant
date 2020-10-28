namespace SCIA
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblStepStatus = new System.Windows.Forms.Label();
            this.chkStepList = new System.Windows.Forms.CheckedListBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabSiteDetails = new System.Windows.Forms.TabControl();
            this.tabPgDbConn = new System.Windows.Forms.TabPage();
            this.txtSqlPass = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSqlUser = new System.Windows.Forms.TextBox();
            this.txtDbServer = new System.Windows.Forms.TextBox();
            this.tabPgSiteInfo = new System.Windows.Forms.TabPage();
            this.label14 = new System.Windows.Forms.Label();
            this.txtxConnectString = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtIdentityServerNameAdditional = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSitePrefixAdditional = new System.Windows.Forms.TextBox();
            this.txtSiteNameSuffix = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPgGeneral = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCommerceEngineConnectClientId = new System.Windows.Forms.TextBox();
            this.txtCommerceEngineConnectClientSecret = new System.Windows.Forms.TextBox();
            this.tabPgInstallDetails = new System.Windows.Forms.TabPage();
            this.txtSiteRootDir = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tabPgSitecore = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSitecoreUserName = new System.Windows.Forms.TextBox();
            this.txtSitecoreDomain = new System.Windows.Forms.TextBox();
            this.tabPgSolr = new System.Windows.Forms.TabPage();
            this.label15 = new System.Windows.Forms.Label();
            this.txtSearchIndexPrefix = new System.Windows.Forms.TextBox();
            this.tabPgRedis = new System.Windows.Forms.TabPage();
            this.txtRedisPort = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtRedisHost = new System.Windows.Forms.TextBox();
            this.tabPgBizFx = new System.Windows.Forms.TabPage();
            this.txtBizFxSitePrefix = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.tabPgEnvironments = new System.Windows.Forms.TabPage();
            this.txtCommerceDbNameString = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtEnvironmentPrefix = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.tabPgWinUser = new System.Windows.Forms.TabPage();
            this.txtUserDomain = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.tabPgBraintree = new System.Windows.Forms.TabPage();
            this.txtBraintreeMerchantId = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.txtBraintreePublicKey = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.txtBraintreePrivateKey = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.txtBraintreeEnvironment = new System.Windows.Forms.TextBox();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabSiteDetails.SuspendLayout();
            this.tabPgDbConn.SuspendLayout();
            this.tabPgSiteInfo.SuspendLayout();
            this.tabPgGeneral.SuspendLayout();
            this.tabPgInstallDetails.SuspendLayout();
            this.tabPgSitecore.SuspendLayout();
            this.tabPgSolr.SuspendLayout();
            this.tabPgRedis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRedisPort)).BeginInit();
            this.tabPgBizFx.SuspendLayout();
            this.tabPgEnvironments.SuspendLayout();
            this.tabPgWinUser.SuspendLayout();
            this.tabPgBraintree.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label5.Location = new System.Drawing.Point(190, 11);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1372, 119);
            this.label5.TabIndex = 8;
            this.label5.Text = "Sitecore Commerce Install Assistant (SCIA) Settings";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1.Location = new System.Drawing.Point(24, 11);
            this.panel1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(158, 119);
            this.panel1.TabIndex = 5;
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnReset.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnReset.Location = new System.Drawing.Point(1201, 664);
            this.btnReset.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(180, 48);
            this.btnReset.TabIndex = 47;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSave.Location = new System.Drawing.Point(1391, 664);
            this.btnSave.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(171, 48);
            this.btnSave.TabIndex = 48;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.EnabledChanged += new System.EventHandler(this.btnSave_EnabledChanged);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblStepStatus
            // 
            this.lblStepStatus.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.lblStepStatus.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblStepStatus.Location = new System.Drawing.Point(1142, 157);
            this.lblStepStatus.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblStepStatus.Name = "lblStepStatus";
            this.lblStepStatus.Size = new System.Drawing.Size(408, 50);
            this.lblStepStatus.TabIndex = 52;
            this.lblStepStatus.Text = "Step 1 of 11: DB Connection";
            this.lblStepStatus.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // chkStepList
            // 
            this.chkStepList.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.chkStepList.CheckOnClick = true;
            this.chkStepList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkStepList.FormattingEnabled = true;
            this.chkStepList.Items.AddRange(new object[] {
            "DB Connection",
            "Site Info",
            "General",
            "Install Details",
            "Sitecore ",
            "Solr",
            "Redis",
            "BizFx",
            "Environments",
            "Win User",
            "Braintree"});
            this.chkStepList.Location = new System.Drawing.Point(0, 10);
            this.chkStepList.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.chkStepList.Name = "chkStepList";
            this.chkStepList.Size = new System.Drawing.Size(290, 368);
            this.chkStepList.TabIndex = 53;
            this.chkStepList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkStepsList_ItemCheck);
            this.chkStepList.Click += new System.EventHandler(this.chkStepsList_Click);
            this.chkStepList.SelectedIndexChanged += new System.EventHandler(this.chkStepList_SelectedIndexChanged);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel3.Controls.Add(this.chkStepList);
            this.panel3.Location = new System.Drawing.Point(1236, 223);
            this.panel3.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(326, 393);
            this.panel3.TabIndex = 54;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.Controls.Add(this.tabSiteDetails);
            this.panel2.Location = new System.Drawing.Point(18, 223);
            this.panel2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1208, 393);
            this.panel2.TabIndex = 15;
            // 
            // tabSiteDetails
            // 
            this.tabSiteDetails.Controls.Add(this.tabPgDbConn);
            this.tabSiteDetails.Controls.Add(this.tabPgSiteInfo);
            this.tabSiteDetails.Controls.Add(this.tabPgGeneral);
            this.tabSiteDetails.Controls.Add(this.tabPgInstallDetails);
            this.tabSiteDetails.Controls.Add(this.tabPgSitecore);
            this.tabSiteDetails.Controls.Add(this.tabPgSolr);
            this.tabSiteDetails.Controls.Add(this.tabPgRedis);
            this.tabSiteDetails.Controls.Add(this.tabPgBizFx);
            this.tabSiteDetails.Controls.Add(this.tabPgEnvironments);
            this.tabSiteDetails.Controls.Add(this.tabPgWinUser);
            this.tabSiteDetails.Controls.Add(this.tabPgBraintree);
            this.tabSiteDetails.Location = new System.Drawing.Point(14, 10);
            this.tabSiteDetails.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tabSiteDetails.Multiline = true;
            this.tabSiteDetails.Name = "tabSiteDetails";
            this.tabSiteDetails.SelectedIndex = 0;
            this.tabSiteDetails.Size = new System.Drawing.Size(1161, 368);
            this.tabSiteDetails.TabIndex = 53;
            // 
            // tabPgDbConn
            // 
            this.tabPgDbConn.BackColor = System.Drawing.SystemColors.Control;
            this.tabPgDbConn.Controls.Add(this.txtSqlPass);
            this.tabPgDbConn.Controls.Add(this.label30);
            this.tabPgDbConn.Controls.Add(this.label3);
            this.tabPgDbConn.Controls.Add(this.label2);
            this.tabPgDbConn.Controls.Add(this.txtSqlUser);
            this.tabPgDbConn.Controls.Add(this.txtDbServer);
            this.tabPgDbConn.Location = new System.Drawing.Point(8, 39);
            this.tabPgDbConn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tabPgDbConn.Name = "tabPgDbConn";
            this.tabPgDbConn.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tabPgDbConn.Size = new System.Drawing.Size(1145, 321);
            this.tabPgDbConn.TabIndex = 0;
            this.tabPgDbConn.Text = "DB Connection";
            // 
            // txtSqlPass
            // 
            this.txtSqlPass.Enabled = false;
            this.txtSqlPass.Location = new System.Drawing.Point(273, 104);
            this.txtSqlPass.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtSqlPass.MaxLength = 15;
            this.txtSqlPass.Name = "txtSqlPass";
            this.txtSqlPass.Size = new System.Drawing.Size(858, 31);
            this.txtSqlPass.TabIndex = 15;
            this.txtSqlPass.UseSystemPasswordChar = true;
            this.txtSqlPass.Leave += new System.EventHandler(this.txtSqlPass_Leave);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(15, 108);
            this.label30.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(97, 25);
            this.label30.TabIndex = 0;
            this.label30.Text = "SqlPass:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 25);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "DbServer:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 66);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "SqlUser:";
            // 
            // txtSqlUser
            // 
            this.txtSqlUser.Enabled = false;
            this.txtSqlUser.Location = new System.Drawing.Point(273, 62);
            this.txtSqlUser.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtSqlUser.MaxLength = 20;
            this.txtSqlUser.Name = "txtSqlUser";
            this.txtSqlUser.Size = new System.Drawing.Size(858, 31);
            this.txtSqlUser.TabIndex = 14;
            this.txtSqlUser.Text = "sa";
            this.txtSqlUser.Leave += new System.EventHandler(this.txtSqlUser_Leave);
            // 
            // txtDbServer
            // 
            this.txtDbServer.Enabled = false;
            this.txtDbServer.Location = new System.Drawing.Point(273, 21);
            this.txtDbServer.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtDbServer.MaxLength = 25;
            this.txtDbServer.Name = "txtDbServer";
            this.txtDbServer.Size = new System.Drawing.Size(858, 31);
            this.txtDbServer.TabIndex = 12;
            this.txtDbServer.Text = "(local)";
            this.txtDbServer.Leave += new System.EventHandler(this.txtSitecoreDbServer_Leave);
            // 
            // tabPgSiteInfo
            // 
            this.tabPgSiteInfo.BackColor = System.Drawing.SystemColors.Control;
            this.tabPgSiteInfo.Controls.Add(this.label14);
            this.tabPgSiteInfo.Controls.Add(this.txtxConnectString);
            this.tabPgSiteInfo.Controls.Add(this.label13);
            this.tabPgSiteInfo.Controls.Add(this.txtIdentityServerNameAdditional);
            this.tabPgSiteInfo.Controls.Add(this.label6);
            this.tabPgSiteInfo.Controls.Add(this.txtSitePrefixAdditional);
            this.tabPgSiteInfo.Controls.Add(this.txtSiteNameSuffix);
            this.tabPgSiteInfo.Controls.Add(this.label1);
            this.tabPgSiteInfo.Location = new System.Drawing.Point(8, 39);
            this.tabPgSiteInfo.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tabPgSiteInfo.Name = "tabPgSiteInfo";
            this.tabPgSiteInfo.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tabPgSiteInfo.Size = new System.Drawing.Size(1145, 321);
            this.tabPgSiteInfo.TabIndex = 1;
            this.tabPgSiteInfo.Text = "Site Info";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(14, 148);
            this.label14.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(284, 25);
            this.label14.TabIndex = 4;
            this.label14.Text = "xConnectServerNameString:";
            // 
            // txtxConnectString
            // 
            this.txtxConnectString.Location = new System.Drawing.Point(312, 145);
            this.txtxConnectString.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtxConnectString.MaxLength = 50;
            this.txtxConnectString.Name = "txtxConnectString";
            this.txtxConnectString.Size = new System.Drawing.Size(818, 31);
            this.txtxConnectString.TabIndex = 5;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(14, 104);
            this.label13.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(262, 25);
            this.label13.TabIndex = 2;
            this.label13.Text = "IdentityServerNameString:";
            // 
            // txtIdentityServerNameAdditional
            // 
            this.txtIdentityServerNameAdditional.Location = new System.Drawing.Point(312, 101);
            this.txtIdentityServerNameAdditional.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtIdentityServerNameAdditional.MaxLength = 50;
            this.txtIdentityServerNameAdditional.Name = "txtIdentityServerNameAdditional";
            this.txtIdentityServerNameAdditional.Size = new System.Drawing.Size(818, 31);
            this.txtIdentityServerNameAdditional.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 65);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(166, 25);
            this.label6.TabIndex = 0;
            this.label6.Text = "SitePrefixString:";
            // 
            // txtSitePrefixAdditional
            // 
            this.txtSitePrefixAdditional.Location = new System.Drawing.Point(312, 61);
            this.txtSitePrefixAdditional.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtSitePrefixAdditional.MaxLength = 50;
            this.txtSitePrefixAdditional.Name = "txtSitePrefixAdditional";
            this.txtSitePrefixAdditional.Size = new System.Drawing.Size(818, 31);
            this.txtSitePrefixAdditional.TabIndex = 1;
            this.txtSitePrefixAdditional.Leave += new System.EventHandler(this.txtSiteNameSuffix_Leave);
            // 
            // txtSiteNameSuffix
            // 
            this.txtSiteNameSuffix.Location = new System.Drawing.Point(312, 23);
            this.txtSiteNameSuffix.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtSiteNameSuffix.MaxLength = 50;
            this.txtSiteNameSuffix.Name = "txtSiteNameSuffix";
            this.txtSiteNameSuffix.Size = new System.Drawing.Size(818, 31);
            this.txtSiteNameSuffix.TabIndex = 1;
            this.txtSiteNameSuffix.Leave += new System.EventHandler(this.txtSiteNameSuffix_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "SiteNameSuffix:";
            // 
            // tabPgGeneral
            // 
            this.tabPgGeneral.Controls.Add(this.label7);
            this.tabPgGeneral.Controls.Add(this.label4);
            this.tabPgGeneral.Controls.Add(this.txtCommerceEngineConnectClientId);
            this.tabPgGeneral.Controls.Add(this.txtCommerceEngineConnectClientSecret);
            this.tabPgGeneral.Location = new System.Drawing.Point(8, 39);
            this.tabPgGeneral.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tabPgGeneral.Name = "tabPgGeneral";
            this.tabPgGeneral.Size = new System.Drawing.Size(1145, 321);
            this.tabPgGeneral.TabIndex = 2;
            this.tabPgGeneral.Text = "General";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 23);
            this.label7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(240, 25);
            this.label7.TabIndex = 0;
            this.label7.Text = "CommEngConnClientId:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 64);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(250, 25);
            this.label4.TabIndex = 0;
            this.label4.Text = "CommEngConnClSecret:";
            // 
            // txtCommerceEngineConnectClientId
            // 
            this.txtCommerceEngineConnectClientId.Location = new System.Drawing.Point(254, 19);
            this.txtCommerceEngineConnectClientId.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtCommerceEngineConnectClientId.MaxLength = 50;
            this.txtCommerceEngineConnectClientId.Name = "txtCommerceEngineConnectClientId";
            this.txtCommerceEngineConnectClientId.Size = new System.Drawing.Size(887, 31);
            this.txtCommerceEngineConnectClientId.TabIndex = 5;
            // 
            // txtCommerceEngineConnectClientSecret
            // 
            this.txtCommerceEngineConnectClientSecret.Location = new System.Drawing.Point(254, 60);
            this.txtCommerceEngineConnectClientSecret.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtCommerceEngineConnectClientSecret.MaxLength = 100;
            this.txtCommerceEngineConnectClientSecret.Name = "txtCommerceEngineConnectClientSecret";
            this.txtCommerceEngineConnectClientSecret.Size = new System.Drawing.Size(887, 31);
            this.txtCommerceEngineConnectClientSecret.TabIndex = 6;
            this.txtCommerceEngineConnectClientSecret.Leave += new System.EventHandler(this.txtCommerceEngineConnectClientSecret_Leave);
            // 
            // tabPgInstallDetails
            // 
            this.tabPgInstallDetails.Controls.Add(this.txtSiteRootDir);
            this.tabPgInstallDetails.Controls.Add(this.label8);
            this.tabPgInstallDetails.Location = new System.Drawing.Point(8, 39);
            this.tabPgInstallDetails.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tabPgInstallDetails.Name = "tabPgInstallDetails";
            this.tabPgInstallDetails.Size = new System.Drawing.Size(1145, 321);
            this.tabPgInstallDetails.TabIndex = 3;
            this.tabPgInstallDetails.Text = "Install Details";
            // 
            // txtSiteRootDir
            // 
            this.txtSiteRootDir.Location = new System.Drawing.Point(276, 19);
            this.txtSiteRootDir.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtSiteRootDir.MaxLength = 200;
            this.txtSiteRootDir.Name = "txtSiteRootDir";
            this.txtSiteRootDir.Size = new System.Drawing.Size(861, 31);
            this.txtSiteRootDir.TabIndex = 8;
            this.txtSiteRootDir.Leave += new System.EventHandler(this.txtSXAInstallDir_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 23);
            this.label8.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(127, 25);
            this.label8.TabIndex = 0;
            this.label8.Text = "SiteRootDir:";
            // 
            // tabPgSitecore
            // 
            this.tabPgSitecore.Controls.Add(this.label10);
            this.tabPgSitecore.Controls.Add(this.label9);
            this.tabPgSitecore.Controls.Add(this.txtSitecoreUserName);
            this.tabPgSitecore.Controls.Add(this.txtSitecoreDomain);
            this.tabPgSitecore.Location = new System.Drawing.Point(8, 39);
            this.tabPgSitecore.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tabPgSitecore.Name = "tabPgSitecore";
            this.tabPgSitecore.Size = new System.Drawing.Size(1145, 321);
            this.tabPgSitecore.TabIndex = 4;
            this.tabPgSitecore.Text = "Sitecore";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 23);
            this.label10.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(170, 25);
            this.label10.TabIndex = 0;
            this.label10.Text = "SitecoreDomain:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 64);
            this.label9.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(198, 25);
            this.label9.TabIndex = 0;
            this.label9.Text = "SitecoreUserName:";
            // 
            // txtSitecoreUserName
            // 
            this.txtSitecoreUserName.Location = new System.Drawing.Point(271, 60);
            this.txtSitecoreUserName.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtSitecoreUserName.MaxLength = 15;
            this.txtSitecoreUserName.Name = "txtSitecoreUserName";
            this.txtSitecoreUserName.Size = new System.Drawing.Size(859, 31);
            this.txtSitecoreUserName.TabIndex = 17;
            this.txtSitecoreUserName.Leave += new System.EventHandler(this.txtSitecoreUsername_Leave);
            // 
            // txtSitecoreDomain
            // 
            this.txtSitecoreDomain.Location = new System.Drawing.Point(271, 19);
            this.txtSitecoreDomain.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtSitecoreDomain.MaxLength = 20;
            this.txtSitecoreDomain.Name = "txtSitecoreDomain";
            this.txtSitecoreDomain.Size = new System.Drawing.Size(859, 31);
            this.txtSitecoreDomain.TabIndex = 16;
            this.txtSitecoreDomain.Leave += new System.EventHandler(this.txtSitecoreDomain_Leave);
            // 
            // tabPgSolr
            // 
            this.tabPgSolr.Controls.Add(this.label15);
            this.tabPgSolr.Controls.Add(this.txtSearchIndexPrefix);
            this.tabPgSolr.Location = new System.Drawing.Point(8, 39);
            this.tabPgSolr.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tabPgSolr.Name = "tabPgSolr";
            this.tabPgSolr.Size = new System.Drawing.Size(1145, 321);
            this.tabPgSolr.TabIndex = 5;
            this.tabPgSolr.Text = "Solr";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(3, 23);
            this.label15.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(193, 25);
            this.label15.TabIndex = 0;
            this.label15.Text = "SearchIndexPrefix:";
            // 
            // txtSearchIndexPrefix
            // 
            this.txtSearchIndexPrefix.Location = new System.Drawing.Point(250, 20);
            this.txtSearchIndexPrefix.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtSearchIndexPrefix.MaxLength = 50;
            this.txtSearchIndexPrefix.Name = "txtSearchIndexPrefix";
            this.txtSearchIndexPrefix.Size = new System.Drawing.Size(886, 31);
            this.txtSearchIndexPrefix.TabIndex = 19;
            this.txtSearchIndexPrefix.Leave += new System.EventHandler(this.txtSearchIndexPrefix_Leave);
            // 
            // tabPgRedis
            // 
            this.tabPgRedis.Controls.Add(this.txtRedisPort);
            this.tabPgRedis.Controls.Add(this.label17);
            this.tabPgRedis.Controls.Add(this.label16);
            this.tabPgRedis.Controls.Add(this.txtRedisHost);
            this.tabPgRedis.Location = new System.Drawing.Point(8, 39);
            this.tabPgRedis.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tabPgRedis.Name = "tabPgRedis";
            this.tabPgRedis.Size = new System.Drawing.Size(1145, 321);
            this.tabPgRedis.TabIndex = 6;
            this.tabPgRedis.Text = "Redis";
            // 
            // txtRedisPort
            // 
            this.txtRedisPort.Location = new System.Drawing.Point(271, 60);
            this.txtRedisPort.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtRedisPort.Maximum = new decimal(new int[] {
            49000,
            0,
            0,
            0});
            this.txtRedisPort.Name = "txtRedisPort";
            this.txtRedisPort.Size = new System.Drawing.Size(225, 31);
            this.txtRedisPort.TabIndex = 25;
            this.txtRedisPort.Value = new decimal(new int[] {
            6379,
            0,
            0,
            0});
            this.txtRedisPort.Leave += new System.EventHandler(this.txtRedisPort_Leave);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(14, 21);
            this.label17.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(117, 25);
            this.label17.TabIndex = 0;
            this.label17.Text = "RedisHost:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(14, 62);
            this.label16.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(112, 25);
            this.label16.TabIndex = 0;
            this.label16.Text = "RedisPort:";
            // 
            // txtRedisHost
            // 
            this.txtRedisHost.Location = new System.Drawing.Point(271, 17);
            this.txtRedisHost.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtRedisHost.MaxLength = 50;
            this.txtRedisHost.Name = "txtRedisHost";
            this.txtRedisHost.Size = new System.Drawing.Size(865, 31);
            this.txtRedisHost.TabIndex = 24;
            this.txtRedisHost.Leave += new System.EventHandler(this.txtRedisHost_Leave);
            // 
            // tabPgBizFx
            // 
            this.tabPgBizFx.Controls.Add(this.txtBizFxSitePrefix);
            this.tabPgBizFx.Controls.Add(this.label22);
            this.tabPgBizFx.Location = new System.Drawing.Point(8, 39);
            this.tabPgBizFx.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tabPgBizFx.Name = "tabPgBizFx";
            this.tabPgBizFx.Size = new System.Drawing.Size(1145, 321);
            this.tabPgBizFx.TabIndex = 7;
            this.tabPgBizFx.Text = "BizFx";
            // 
            // txtBizFxSitePrefix
            // 
            this.txtBizFxSitePrefix.Location = new System.Drawing.Point(296, 20);
            this.txtBizFxSitePrefix.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtBizFxSitePrefix.MaxLength = 100;
            this.txtBizFxSitePrefix.Name = "txtBizFxSitePrefix";
            this.txtBizFxSitePrefix.Size = new System.Drawing.Size(849, 31);
            this.txtBizFxSitePrefix.TabIndex = 36;
            this.txtBizFxSitePrefix.Leave += new System.EventHandler(this.txtBizFxName_Leave);
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(8, 23);
            this.label22.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(237, 27);
            this.label22.TabIndex = 0;
            this.label22.Text = "BizFxSiteNamePrefix:";
            // 
            // tabPgEnvironments
            // 
            this.tabPgEnvironments.Controls.Add(this.txtCommerceDbNameString);
            this.tabPgEnvironments.Controls.Add(this.label12);
            this.tabPgEnvironments.Controls.Add(this.txtEnvironmentPrefix);
            this.tabPgEnvironments.Controls.Add(this.label24);
            this.tabPgEnvironments.Location = new System.Drawing.Point(8, 39);
            this.tabPgEnvironments.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tabPgEnvironments.Name = "tabPgEnvironments";
            this.tabPgEnvironments.Size = new System.Drawing.Size(1145, 321);
            this.tabPgEnvironments.TabIndex = 8;
            this.tabPgEnvironments.Text = "Environments";
            // 
            // txtCommerceDbNameString
            // 
            this.txtCommerceDbNameString.Location = new System.Drawing.Point(298, 59);
            this.txtCommerceDbNameString.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtCommerceDbNameString.Name = "txtCommerceDbNameString";
            this.txtCommerceDbNameString.Size = new System.Drawing.Size(832, 31);
            this.txtCommerceDbNameString.TabIndex = 39;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(10, 62);
            this.label12.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(256, 25);
            this.label12.TabIndex = 38;
            this.label12.Text = "CommerceDBNameString";
            // 
            // txtEnvironmentPrefix
            // 
            this.txtEnvironmentPrefix.Location = new System.Drawing.Point(298, 20);
            this.txtEnvironmentPrefix.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtEnvironmentPrefix.Name = "txtEnvironmentPrefix";
            this.txtEnvironmentPrefix.Size = new System.Drawing.Size(832, 31);
            this.txtEnvironmentPrefix.TabIndex = 37;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(10, 23);
            this.label24.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(204, 25);
            this.label24.TabIndex = 0;
            this.label24.Text = "EnvironmentsPrefix:";
            // 
            // tabPgWinUser
            // 
            this.tabPgWinUser.Controls.Add(this.txtUserDomain);
            this.tabPgWinUser.Controls.Add(this.label25);
            this.tabPgWinUser.Location = new System.Drawing.Point(8, 39);
            this.tabPgWinUser.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tabPgWinUser.Name = "tabPgWinUser";
            this.tabPgWinUser.Size = new System.Drawing.Size(1145, 321);
            this.tabPgWinUser.TabIndex = 9;
            this.tabPgWinUser.Text = "Win User";
            // 
            // txtUserDomain
            // 
            this.txtUserDomain.Location = new System.Drawing.Point(269, 21);
            this.txtUserDomain.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtUserDomain.MaxLength = 50;
            this.txtUserDomain.Name = "txtUserDomain";
            this.txtUserDomain.Size = new System.Drawing.Size(862, 31);
            this.txtUserDomain.TabIndex = 39;
            this.txtUserDomain.Leave += new System.EventHandler(this.txtUserDomain_Leave);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(10, 25);
            this.label25.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(136, 25);
            this.label25.TabIndex = 0;
            this.label25.Text = "UserDomain:";
            // 
            // tabPgBraintree
            // 
            this.tabPgBraintree.Controls.Add(this.txtBraintreeMerchantId);
            this.tabPgBraintree.Controls.Add(this.label29);
            this.tabPgBraintree.Controls.Add(this.txtBraintreePublicKey);
            this.tabPgBraintree.Controls.Add(this.label28);
            this.tabPgBraintree.Controls.Add(this.txtBraintreePrivateKey);
            this.tabPgBraintree.Controls.Add(this.label27);
            this.tabPgBraintree.Controls.Add(this.label26);
            this.tabPgBraintree.Controls.Add(this.txtBraintreeEnvironment);
            this.tabPgBraintree.Location = new System.Drawing.Point(8, 39);
            this.tabPgBraintree.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tabPgBraintree.Name = "tabPgBraintree";
            this.tabPgBraintree.Size = new System.Drawing.Size(1145, 321);
            this.tabPgBraintree.TabIndex = 10;
            this.tabPgBraintree.Text = "Braintree";
            // 
            // txtBraintreeMerchantId
            // 
            this.txtBraintreeMerchantId.Location = new System.Drawing.Point(264, 19);
            this.txtBraintreeMerchantId.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtBraintreeMerchantId.MaxLength = 100;
            this.txtBraintreeMerchantId.Name = "txtBraintreeMerchantId";
            this.txtBraintreeMerchantId.Size = new System.Drawing.Size(878, 31);
            this.txtBraintreeMerchantId.TabIndex = 42;
            this.txtBraintreeMerchantId.Leave += new System.EventHandler(this.txttxtBraintreeMerchantId_Leave);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(6, 23);
            this.label29.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(212, 25);
            this.label29.TabIndex = 0;
            this.label29.Text = "BraintreeMerchantId:";
            // 
            // txtBraintreePublicKey
            // 
            this.txtBraintreePublicKey.Location = new System.Drawing.Point(264, 59);
            this.txtBraintreePublicKey.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtBraintreePublicKey.MaxLength = 100;
            this.txtBraintreePublicKey.Name = "txtBraintreePublicKey";
            this.txtBraintreePublicKey.Size = new System.Drawing.Size(878, 31);
            this.txtBraintreePublicKey.TabIndex = 43;
            this.txtBraintreePublicKey.Leave += new System.EventHandler(this.txtBraintreePublicKey_Leave);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(6, 64);
            this.label28.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(201, 25);
            this.label28.TabIndex = 0;
            this.label28.Text = "BraintreePublicKey:";
            // 
            // txtBraintreePrivateKey
            // 
            this.txtBraintreePrivateKey.Location = new System.Drawing.Point(264, 100);
            this.txtBraintreePrivateKey.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtBraintreePrivateKey.MaxLength = 100;
            this.txtBraintreePrivateKey.Name = "txtBraintreePrivateKey";
            this.txtBraintreePrivateKey.Size = new System.Drawing.Size(878, 31);
            this.txtBraintreePrivateKey.TabIndex = 44;
            this.txtBraintreePrivateKey.Leave += new System.EventHandler(this.txtBraintreePrivateKey_Leave);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(6, 104);
            this.label27.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(209, 25);
            this.label27.TabIndex = 0;
            this.label27.Text = "BraintreePrivateKey:";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(6, 145);
            this.label26.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(225, 25);
            this.label26.TabIndex = 0;
            this.label26.Text = "BraintreeEnvironment:";
            // 
            // txtBraintreeEnvironment
            // 
            this.txtBraintreeEnvironment.Location = new System.Drawing.Point(264, 141);
            this.txtBraintreeEnvironment.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtBraintreeEnvironment.MaxLength = 100;
            this.txtBraintreeEnvironment.Name = "txtBraintreeEnvironment";
            this.txtBraintreeEnvironment.Size = new System.Drawing.Size(878, 31);
            this.txtBraintreeEnvironment.TabIndex = 45;
            this.txtBraintreeEnvironment.Leave += new System.EventHandler(this.txtBraintreeEnvironment_Leave);
            // 
            // btnPrevious
            // 
            this.btnPrevious.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrevious.BackgroundImage")));
            this.btnPrevious.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPrevious.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrevious.Location = new System.Drawing.Point(93, 152);
            this.btnPrevious.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(70, 55);
            this.btnPrevious.TabIndex = 55;
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnFirst.BackgroundImage")));
            this.btnFirst.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnFirst.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFirst.Location = new System.Drawing.Point(28, 152);
            this.btnFirst.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(70, 55);
            this.btnFirst.TabIndex = 56;
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnLast
            // 
            this.btnLast.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLast.BackgroundImage")));
            this.btnLast.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnLast.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLast.Location = new System.Drawing.Point(224, 152);
            this.btnLast.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(70, 55);
            this.btnLast.TabIndex = 57;
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnNext.BackgroundImage")));
            this.btnNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNext.Location = new System.Drawing.Point(160, 152);
            this.btnNext.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(70, 55);
            this.btnNext.TabIndex = 58;
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblStatus.Location = new System.Drawing.Point(21, 718);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(948, 38);
            this.lblStatus.TabIndex = 62;
            this.lblStatus.Text = "Happy Sitecoring!";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1584, 765);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnFirst);
            this.Controls.Add(this.btnLast);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.lblStepStatus);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label5);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.Text = "Sitecore Commerce Install Assistant (SCIA) - Settings";
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tabSiteDetails.ResumeLayout(false);
            this.tabPgDbConn.ResumeLayout(false);
            this.tabPgDbConn.PerformLayout();
            this.tabPgSiteInfo.ResumeLayout(false);
            this.tabPgSiteInfo.PerformLayout();
            this.tabPgGeneral.ResumeLayout(false);
            this.tabPgGeneral.PerformLayout();
            this.tabPgInstallDetails.ResumeLayout(false);
            this.tabPgInstallDetails.PerformLayout();
            this.tabPgSitecore.ResumeLayout(false);
            this.tabPgSitecore.PerformLayout();
            this.tabPgSolr.ResumeLayout(false);
            this.tabPgSolr.PerformLayout();
            this.tabPgRedis.ResumeLayout(false);
            this.tabPgRedis.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRedisPort)).EndInit();
            this.tabPgBizFx.ResumeLayout(false);
            this.tabPgBizFx.PerformLayout();
            this.tabPgEnvironments.ResumeLayout(false);
            this.tabPgEnvironments.PerformLayout();
            this.tabPgWinUser.ResumeLayout(false);
            this.tabPgWinUser.PerformLayout();
            this.tabPgBraintree.ResumeLayout(false);
            this.tabPgBraintree.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblStepStatus;
        private System.Windows.Forms.CheckedListBox chkStepList;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tabSiteDetails;
        private System.Windows.Forms.TabPage tabPgDbConn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSqlUser;
        private System.Windows.Forms.TextBox txtDbServer;
        private System.Windows.Forms.TabPage tabPgSiteInfo;
        private System.Windows.Forms.TextBox txtSiteNameSuffix;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPgGeneral;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCommerceEngineConnectClientId;
        private System.Windows.Forms.TextBox txtCommerceEngineConnectClientSecret;
        private System.Windows.Forms.TabPage tabPgInstallDetails;
        private System.Windows.Forms.TextBox txtSiteRootDir;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TabPage tabPgSitecore;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtSitecoreUserName;
        private System.Windows.Forms.TextBox txtSitecoreDomain;
        private System.Windows.Forms.TabPage tabPgSolr;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtSearchIndexPrefix;
        private System.Windows.Forms.TabPage tabPgRedis;
        private System.Windows.Forms.NumericUpDown txtRedisPort;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtRedisHost;
        private System.Windows.Forms.TabPage tabPgBizFx;
        private System.Windows.Forms.TextBox txtBizFxSitePrefix;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TabPage tabPgEnvironments;
        private System.Windows.Forms.TextBox txtEnvironmentPrefix;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TabPage tabPgWinUser;
        private System.Windows.Forms.TextBox txtUserDomain;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TabPage tabPgBraintree;
        private System.Windows.Forms.TextBox txtBraintreeMerchantId;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox txtBraintreePublicKey;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox txtBraintreePrivateKey;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox txtBraintreeEnvironment;
        private System.Windows.Forms.TextBox txtSqlPass;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSitePrefixAdditional;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.TextBox txtCommerceDbNameString;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtxConnectString;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtIdentityServerNameAdditional;
        private System.Windows.Forms.Label lblStatus;
    }
}