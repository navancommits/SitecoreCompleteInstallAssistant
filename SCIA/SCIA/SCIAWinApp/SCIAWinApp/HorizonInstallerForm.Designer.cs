namespace SCIA
{
    partial class HorizonInstallerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HorizonInstallerForm));
            this.btnScriptPreview = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.lblStepInfo = new System.Windows.Forms.Label();
            this.btnFirst = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnValidateAll = new System.Windows.Forms.Button();
            this.btnSolr = new System.Windows.Forms.Button();
            this.btnDbConn = new System.Windows.Forms.Button();
            this.btnAppSettings = new System.Windows.Forms.Button();
            this.btnPrerequisites = new System.Windows.Forms.Button();
            this.label25 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkStepsList = new System.Windows.Forms.CheckedListBox();
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
            this.label10 = new System.Windows.Forms.Label();
            this.txtSiteUrl = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtHorizonAppUrl = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtHorizonInstanceName = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.txtSiteNameSuffix = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtSiteNamePrefix = new System.Windows.Forms.TextBox();
            this.txtSiteName = new System.Windows.Forms.TextBox();
            this.tabPgGeneral = new System.Windows.Forms.TabPage();
            this.txtIDServerAppPoolName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSitecoreIdentityServerUrl = new System.Windows.Forms.TextBox();
            this.txtIDServerSiteName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPgInstallDetails = new System.Windows.Forms.TabPage();
            this.txtIDServerInstallDir = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtHorizonInstallDir = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSXAInstallDir = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPgSitecore = new System.Windows.Forms.TabPage();
            this.label16 = new System.Windows.Forms.Label();
            this.txtSitecoreUserPassword = new System.Windows.Forms.TextBox();
            this.txtSitecoreUsername = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tabPgSolr = new System.Windows.Forms.TabPage();
            this.txtSolrVersion = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.txtSolrService = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtSolrRoot = new System.Windows.Forms.TextBox();
            this.txtSolrUrl = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnInstall = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.toolTipSIF = new System.Windows.Forms.ToolTip(this.components);
            this.panel2.SuspendLayout();
            this.pnlDetails.SuspendLayout();
            this.tabDetails.SuspendLayout();
            this.tabPgDBConnection.SuspendLayout();
            this.tabpgSiteInfo.SuspendLayout();
            this.tabPgGeneral.SuspendLayout();
            this.tabPgInstallDetails.SuspendLayout();
            this.tabPgSitecore.SuspendLayout();
            this.tabPgSolr.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnScriptPreview
            // 
            this.btnScriptPreview.BackColor = System.Drawing.Color.White;
            this.btnScriptPreview.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnScriptPreview.BackgroundImage")));
            this.btnScriptPreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnScriptPreview.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnScriptPreview.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnScriptPreview.Location = new System.Drawing.Point(342, 175);
            this.btnScriptPreview.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnScriptPreview.Name = "btnScriptPreview";
            this.btnScriptPreview.Size = new System.Drawing.Size(70, 55);
            this.btnScriptPreview.TabIndex = 70;
            this.toolTipSIF.SetToolTip(this.btnScriptPreview, "Preview of PS Script Generated");
            this.btnScriptPreview.UseVisualStyleBackColor = false;
            // 
            // btnPrevious
            // 
            this.btnPrevious.BackColor = System.Drawing.Color.White;
            this.btnPrevious.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrevious.BackgroundImage")));
            this.btnPrevious.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPrevious.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrevious.Location = new System.Drawing.Point(478, 175);
            this.btnPrevious.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(70, 55);
            this.btnPrevious.TabIndex = 59;
            this.toolTipSIF.SetToolTip(this.btnPrevious, "Show Previous Tab");
            this.btnPrevious.UseVisualStyleBackColor = false;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // lblStepInfo
            // 
            this.lblStepInfo.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.lblStepInfo.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblStepInfo.Location = new System.Drawing.Point(1152, 190);
            this.lblStepInfo.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblStepInfo.Name = "lblStepInfo";
            this.lblStepInfo.Size = new System.Drawing.Size(408, 40);
            this.lblStepInfo.TabIndex = 69;
            this.lblStepInfo.Text = "Step 1 of 6: DB Connection";
            this.lblStepInfo.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnFirst
            // 
            this.btnFirst.BackColor = System.Drawing.Color.White;
            this.btnFirst.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnFirst.BackgroundImage")));
            this.btnFirst.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnFirst.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFirst.Location = new System.Drawing.Point(413, 175);
            this.btnFirst.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(70, 55);
            this.btnFirst.TabIndex = 60;
            this.toolTipSIF.SetToolTip(this.btnFirst, "Show First Tab");
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
            this.btnLast.Location = new System.Drawing.Point(609, 175);
            this.btnLast.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(70, 55);
            this.btnLast.TabIndex = 61;
            this.toolTipSIF.SetToolTip(this.btnLast, "Show Last Tab");
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
            this.btnNext.Location = new System.Drawing.Point(545, 175);
            this.btnNext.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(70, 55);
            this.btnNext.TabIndex = 62;
            this.toolTipSIF.SetToolTip(this.btnNext, "Show Next Tab");
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnValidateAll
            // 
            this.btnValidateAll.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnValidateAll.BackgroundImage")));
            this.btnValidateAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnValidateAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnValidateAll.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnValidateAll.Location = new System.Drawing.Point(271, 175);
            this.btnValidateAll.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnValidateAll.Name = "btnValidateAll";
            this.btnValidateAll.Size = new System.Drawing.Size(70, 55);
            this.btnValidateAll.TabIndex = 63;
            this.toolTipSIF.SetToolTip(this.btnValidateAll, "Validates All Fields and opens up the Generate, Install and Uninstall buttons");
            this.btnValidateAll.UseVisualStyleBackColor = true;
            this.btnValidateAll.Click += new System.EventHandler(this.btnValidateAll_Click);
            // 
            // btnSolr
            // 
            this.btnSolr.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSolr.BackgroundImage")));
            this.btnSolr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSolr.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSolr.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSolr.Location = new System.Drawing.Point(214, 175);
            this.btnSolr.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnSolr.Name = "btnSolr";
            this.btnSolr.Size = new System.Drawing.Size(70, 55);
            this.btnSolr.TabIndex = 65;
            this.toolTipSIF.SetToolTip(this.btnSolr, "Solr Check");
            this.btnSolr.UseVisualStyleBackColor = true;
            this.btnSolr.Click += new System.EventHandler(this.btnSolr_Click);
            // 
            // btnDbConn
            // 
            this.btnDbConn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDbConn.BackgroundImage")));
            this.btnDbConn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDbConn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDbConn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDbConn.Location = new System.Drawing.Point(149, 175);
            this.btnDbConn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnDbConn.Name = "btnDbConn";
            this.btnDbConn.Size = new System.Drawing.Size(70, 55);
            this.btnDbConn.TabIndex = 66;
            this.toolTipSIF.SetToolTip(this.btnDbConn, "DB Connection Check");
            this.btnDbConn.UseVisualStyleBackColor = true;
            this.btnDbConn.Click += new System.EventHandler(this.btnDbConn_Click);
            // 
            // btnAppSettings
            // 
            this.btnAppSettings.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAppSettings.BackgroundImage")));
            this.btnAppSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAppSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAppSettings.Enabled = false;
            this.btnAppSettings.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAppSettings.Location = new System.Drawing.Point(19, 175);
            this.btnAppSettings.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnAppSettings.Name = "btnAppSettings";
            this.btnAppSettings.Size = new System.Drawing.Size(70, 55);
            this.btnAppSettings.TabIndex = 68;
            this.toolTipSIF.SetToolTip(this.btnAppSettings, "Show Settings Form");
            this.btnAppSettings.UseVisualStyleBackColor = true;
            this.btnAppSettings.Click += new System.EventHandler(this.btnAppSettings_Click);
            // 
            // btnPrerequisites
            // 
            this.btnPrerequisites.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrerequisites.BackgroundImage")));
            this.btnPrerequisites.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPrerequisites.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrerequisites.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrerequisites.Location = new System.Drawing.Point(84, 175);
            this.btnPrerequisites.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnPrerequisites.Name = "btnPrerequisites";
            this.btnPrerequisites.Size = new System.Drawing.Size(70, 55);
            this.btnPrerequisites.TabIndex = 67;
            this.toolTipSIF.SetToolTip(this.btnPrerequisites, "Show Prerequisites");
            this.btnPrerequisites.UseVisualStyleBackColor = true;
            this.btnPrerequisites.Click += new System.EventHandler(this.btnPrerequisites_Click);
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label25.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label25.Location = new System.Drawing.Point(183, 31);
            this.label25.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(1402, 115);
            this.label25.TabIndex = 57;
            this.label25.Text = "Horizon Installer";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1.Location = new System.Drawing.Point(16, 28);
            this.panel1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(158, 119);
            this.panel1.TabIndex = 56;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.chkStepsList);
            this.panel2.Location = new System.Drawing.Point(1241, 263);
            this.panel2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(344, 413);
            this.panel2.TabIndex = 72;
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
            "General",
            "Install Details",
            "Sitecore ",
            "Solr"});
            this.chkStepsList.Location = new System.Drawing.Point(5, -2);
            this.chkStepsList.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.chkStepsList.Name = "chkStepsList";
            this.chkStepsList.Size = new System.Drawing.Size(290, 396);
            this.chkStepsList.TabIndex = 53;
            this.toolTipSIF.SetToolTip(this.chkStepsList, "Click on List Item to load corresponding Tab");
            this.chkStepsList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkStepsList_ItemCheck);
            this.chkStepsList.Click += new System.EventHandler(this.chkStepsList_Click);
            // 
            // pnlDetails
            // 
            this.pnlDetails.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlDetails.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlDetails.Controls.Add(this.lblStepStatus);
            this.pnlDetails.Controls.Add(this.tabDetails);
            this.pnlDetails.Location = new System.Drawing.Point(19, 263);
            this.pnlDetails.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.pnlDetails.Name = "pnlDetails";
            this.pnlDetails.Size = new System.Drawing.Size(1211, 413);
            this.pnlDetails.TabIndex = 71;
            // 
            // lblStepStatus
            // 
            this.lblStepStatus.AutoSize = true;
            this.lblStepStatus.Location = new System.Drawing.Point(1037, -98);
            this.lblStepStatus.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblStepStatus.Name = "lblStepStatus";
            this.lblStepStatus.Size = new System.Drawing.Size(123, 25);
            this.lblStepStatus.TabIndex = 47;
            this.lblStepStatus.Text = "Step Status";
            // 
            // tabDetails
            // 
            this.tabDetails.Controls.Add(this.tabPgDBConnection);
            this.tabDetails.Controls.Add(this.tabpgSiteInfo);
            this.tabDetails.Controls.Add(this.tabPgGeneral);
            this.tabDetails.Controls.Add(this.tabPgInstallDetails);
            this.tabDetails.Controls.Add(this.tabPgSitecore);
            this.tabDetails.Controls.Add(this.tabPgSolr);
            this.tabDetails.Location = new System.Drawing.Point(6, -2);
            this.tabDetails.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tabDetails.Multiline = true;
            this.tabDetails.Name = "tabDetails";
            this.tabDetails.SelectedIndex = 0;
            this.tabDetails.Size = new System.Drawing.Size(1174, 396);
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
            this.tabPgDBConnection.Location = new System.Drawing.Point(8, 39);
            this.tabPgDBConnection.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tabPgDBConnection.Name = "tabPgDBConnection";
            this.tabPgDBConnection.Size = new System.Drawing.Size(1158, 349);
            this.tabPgDBConnection.TabIndex = 13;
            this.tabPgDBConnection.Text = "DB Connection";
            // 
            // txtSqlPass
            // 
            this.txtSqlPass.Enabled = false;
            this.txtSqlPass.Location = new System.Drawing.Point(269, 104);
            this.txtSqlPass.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtSqlPass.MaxLength = 15;
            this.txtSqlPass.Name = "txtSqlPass";
            this.txtSqlPass.Size = new System.Drawing.Size(871, 31);
            this.txtSqlPass.TabIndex = 50;
            this.toolTipSIF.SetToolTip(this.txtSqlPass, "Enter the DB password and click the DB Check button to go to next tab");
            this.txtSqlPass.UseSystemPasswordChar = true;
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(10, 108);
            this.label49.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(97, 25);
            this.label49.TabIndex = 0;
            this.label49.Text = "SqlPass:";
            // 
            // txtSqlUser
            // 
            this.txtSqlUser.Enabled = false;
            this.txtSqlUser.Location = new System.Drawing.Point(269, 62);
            this.txtSqlUser.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtSqlUser.MaxLength = 20;
            this.txtSqlUser.Name = "txtSqlUser";
            this.txtSqlUser.Size = new System.Drawing.Size(871, 31);
            this.txtSqlUser.TabIndex = 14;
            this.txtSqlUser.Text = "sa";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(10, 66);
            this.label48.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(94, 25);
            this.label48.TabIndex = 0;
            this.label48.Text = "SqlUser:";
            // 
            // txtSqlDbServer
            // 
            this.txtSqlDbServer.Enabled = false;
            this.txtSqlDbServer.Location = new System.Drawing.Point(269, 21);
            this.txtSqlDbServer.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtSqlDbServer.MaxLength = 100;
            this.txtSqlDbServer.Name = "txtSqlDbServer";
            this.txtSqlDbServer.Size = new System.Drawing.Size(871, 31);
            this.txtSqlDbServer.TabIndex = 12;
            this.txtSqlDbServer.Text = "(local)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "DbServer:";
            // 
            // tabpgSiteInfo
            // 
            this.tabpgSiteInfo.Controls.Add(this.label10);
            this.tabpgSiteInfo.Controls.Add(this.txtSiteUrl);
            this.tabpgSiteInfo.Controls.Add(this.label9);
            this.tabpgSiteInfo.Controls.Add(this.txtHorizonAppUrl);
            this.tabpgSiteInfo.Controls.Add(this.label8);
            this.tabpgSiteInfo.Controls.Add(this.txtHorizonInstanceName);
            this.tabpgSiteInfo.Controls.Add(this.label38);
            this.tabpgSiteInfo.Controls.Add(this.label26);
            this.tabpgSiteInfo.Controls.Add(this.txtSiteNameSuffix);
            this.tabpgSiteInfo.Controls.Add(this.label22);
            this.tabpgSiteInfo.Controls.Add(this.txtSiteNamePrefix);
            this.tabpgSiteInfo.Controls.Add(this.txtSiteName);
            this.tabpgSiteInfo.Location = new System.Drawing.Point(8, 39);
            this.tabpgSiteInfo.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tabpgSiteInfo.Name = "tabpgSiteInfo";
            this.tabpgSiteInfo.Size = new System.Drawing.Size(1158, 349);
            this.tabpgSiteInfo.TabIndex = 12;
            this.tabpgSiteInfo.Text = "Site Info";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 138);
            this.label10.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 25);
            this.label10.TabIndex = 13;
            this.label10.Text = "SiteUrl:";
            // 
            // txtSiteUrl
            // 
            this.txtSiteUrl.Enabled = false;
            this.txtSiteUrl.Location = new System.Drawing.Point(264, 134);
            this.txtSiteUrl.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtSiteUrl.MaxLength = 100;
            this.txtSiteUrl.Name = "txtSiteUrl";
            this.txtSiteUrl.Size = new System.Drawing.Size(875, 31);
            this.txtSiteUrl.TabIndex = 12;
            this.txtSiteUrl.Text = "scom10sxa.dev.local";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 215);
            this.label9.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(157, 25);
            this.label9.TabIndex = 10;
            this.label9.Text = "HorizonAppUrl:";
            // 
            // txtHorizonAppUrl
            // 
            this.txtHorizonAppUrl.Enabled = false;
            this.txtHorizonAppUrl.Location = new System.Drawing.Point(264, 212);
            this.txtHorizonAppUrl.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtHorizonAppUrl.MaxLength = 200;
            this.txtHorizonAppUrl.Name = "txtHorizonAppUrl";
            this.txtHorizonAppUrl.Size = new System.Drawing.Size(875, 31);
            this.txtHorizonAppUrl.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 177);
            this.label8.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(185, 25);
            this.label8.TabIndex = 9;
            this.label8.Text = "HorizonSiteName:";
            // 
            // txtHorizonInstanceName
            // 
            this.txtHorizonInstanceName.Enabled = false;
            this.txtHorizonInstanceName.Location = new System.Drawing.Point(264, 173);
            this.txtHorizonInstanceName.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtHorizonInstanceName.MaxLength = 100;
            this.txtHorizonInstanceName.Name = "txtHorizonInstanceName";
            this.txtHorizonInstanceName.Size = new System.Drawing.Size(875, 31);
            this.txtHorizonInstanceName.TabIndex = 8;
            this.txtHorizonInstanceName.Text = "scom10sxa.dev.local";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(3, 21);
            this.label38.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(166, 25);
            this.label38.TabIndex = 0;
            this.label38.Text = "SiteNamePrefix:";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(3, 99);
            this.label26.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(111, 25);
            this.label26.TabIndex = 7;
            this.label26.Text = "SiteName:";
            // 
            // txtSiteNameSuffix
            // 
            this.txtSiteNameSuffix.Enabled = false;
            this.txtSiteNameSuffix.Location = new System.Drawing.Point(264, 56);
            this.txtSiteNameSuffix.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtSiteNameSuffix.MaxLength = 50;
            this.txtSiteNameSuffix.Name = "txtSiteNameSuffix";
            this.txtSiteNameSuffix.Size = new System.Drawing.Size(875, 31);
            this.txtSiteNameSuffix.TabIndex = 1;
            this.txtSiteNameSuffix.TextChanged += new System.EventHandler(this.txtSiteNameSuffix_TextChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(3, 59);
            this.label22.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(165, 25);
            this.label22.TabIndex = 0;
            this.label22.Text = "SiteNameSuffix:";
            // 
            // txtSiteNamePrefix
            // 
            this.txtSiteNamePrefix.Location = new System.Drawing.Point(264, 17);
            this.txtSiteNamePrefix.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtSiteNamePrefix.MaxLength = 50;
            this.txtSiteNamePrefix.Name = "txtSiteNamePrefix";
            this.txtSiteNamePrefix.Size = new System.Drawing.Size(875, 31);
            this.txtSiteNamePrefix.TabIndex = 1;
            this.toolTipSIF.SetToolTip(this.txtSiteNamePrefix, "Enter Site Prefix and press the next button in the menu bar");
            this.txtSiteNamePrefix.TextChanged += new System.EventHandler(this.txtSiteNamePrefix_TextChanged);
            // 
            // txtSiteName
            // 
            this.txtSiteName.Enabled = false;
            this.txtSiteName.Location = new System.Drawing.Point(264, 95);
            this.txtSiteName.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtSiteName.MaxLength = 100;
            this.txtSiteName.Name = "txtSiteName";
            this.txtSiteName.Size = new System.Drawing.Size(875, 31);
            this.txtSiteName.TabIndex = 2;
            this.txtSiteName.TextChanged += new System.EventHandler(this.txtSiteName_TextChanged);
            // 
            // tabPgGeneral
            // 
            this.tabPgGeneral.BackColor = System.Drawing.SystemColors.Control;
            this.tabPgGeneral.Controls.Add(this.txtIDServerAppPoolName);
            this.tabPgGeneral.Controls.Add(this.label3);
            this.tabPgGeneral.Controls.Add(this.label6);
            this.tabPgGeneral.Controls.Add(this.txtSitecoreIdentityServerUrl);
            this.tabPgGeneral.Controls.Add(this.txtIDServerSiteName);
            this.tabPgGeneral.Controls.Add(this.label2);
            this.tabPgGeneral.Location = new System.Drawing.Point(8, 39);
            this.tabPgGeneral.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tabPgGeneral.Name = "tabPgGeneral";
            this.tabPgGeneral.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tabPgGeneral.Size = new System.Drawing.Size(1158, 349);
            this.tabPgGeneral.TabIndex = 0;
            this.tabPgGeneral.Text = "General";
            // 
            // txtIDServerAppPoolName
            // 
            this.txtIDServerAppPoolName.Enabled = false;
            this.txtIDServerAppPoolName.Location = new System.Drawing.Point(269, 108);
            this.txtIDServerAppPoolName.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtIDServerAppPoolName.MaxLength = 50;
            this.txtIDServerAppPoolName.Name = "txtIDServerAppPoolName";
            this.txtIDServerAppPoolName.Size = new System.Drawing.Size(870, 31);
            this.txtIDServerAppPoolName.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 112);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(238, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "IDServerAppPoolName:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 61);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(204, 25);
            this.label6.TabIndex = 0;
            this.label6.Text = "SitecoreIdServerUrl:";
            // 
            // txtSitecoreIdentityServerUrl
            // 
            this.txtSitecoreIdentityServerUrl.Enabled = false;
            this.txtSitecoreIdentityServerUrl.Location = new System.Drawing.Point(269, 58);
            this.txtSitecoreIdentityServerUrl.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtSitecoreIdentityServerUrl.MaxLength = 200;
            this.txtSitecoreIdentityServerUrl.Name = "txtSitecoreIdentityServerUrl";
            this.txtSitecoreIdentityServerUrl.Size = new System.Drawing.Size(870, 31);
            this.txtSitecoreIdentityServerUrl.TabIndex = 4;
            // 
            // txtIDServerSiteName
            // 
            this.txtIDServerSiteName.Enabled = false;
            this.txtIDServerSiteName.Location = new System.Drawing.Point(269, 16);
            this.txtIDServerSiteName.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtIDServerSiteName.MaxLength = 50;
            this.txtIDServerSiteName.Name = "txtIDServerSiteName";
            this.txtIDServerSiteName.Size = new System.Drawing.Size(870, 31);
            this.txtIDServerSiteName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 20);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "IDServerSiteName:";
            // 
            // tabPgInstallDetails
            // 
            this.tabPgInstallDetails.Controls.Add(this.txtIDServerInstallDir);
            this.tabPgInstallDetails.Controls.Add(this.label7);
            this.tabPgInstallDetails.Controls.Add(this.txtHorizonInstallDir);
            this.tabPgInstallDetails.Controls.Add(this.label5);
            this.tabPgInstallDetails.Controls.Add(this.txtSXAInstallDir);
            this.tabPgInstallDetails.Controls.Add(this.label4);
            this.tabPgInstallDetails.Location = new System.Drawing.Point(8, 39);
            this.tabPgInstallDetails.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tabPgInstallDetails.Name = "tabPgInstallDetails";
            this.tabPgInstallDetails.Size = new System.Drawing.Size(1158, 349);
            this.tabPgInstallDetails.TabIndex = 2;
            this.tabPgInstallDetails.Text = "Install Details";
            // 
            // txtIDServerInstallDir
            // 
            this.txtIDServerInstallDir.Enabled = false;
            this.txtIDServerInstallDir.Location = new System.Drawing.Point(269, 112);
            this.txtIDServerInstallDir.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtIDServerInstallDir.MaxLength = 200;
            this.txtIDServerInstallDir.Name = "txtIDServerInstallDir";
            this.txtIDServerInstallDir.Size = new System.Drawing.Size(872, 31);
            this.txtIDServerInstallDir.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 115);
            this.label7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(184, 25);
            this.label7.TabIndex = 10;
            this.label7.Text = "IDServerInstallDir:";
            // 
            // txtHorizonInstallDir
            // 
            this.txtHorizonInstallDir.Enabled = false;
            this.txtHorizonInstallDir.Location = new System.Drawing.Point(269, 64);
            this.txtHorizonInstallDir.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtHorizonInstallDir.MaxLength = 200;
            this.txtHorizonInstallDir.Name = "txtHorizonInstallDir";
            this.txtHorizonInstallDir.Size = new System.Drawing.Size(872, 31);
            this.txtHorizonInstallDir.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 67);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(175, 25);
            this.label5.TabIndex = 0;
            this.label5.Text = "HorizonInstallDir:";
            // 
            // txtSXAInstallDir
            // 
            this.txtSXAInstallDir.Enabled = false;
            this.txtSXAInstallDir.Location = new System.Drawing.Point(269, 23);
            this.txtSXAInstallDir.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtSXAInstallDir.MaxLength = 200;
            this.txtSXAInstallDir.Name = "txtSXAInstallDir";
            this.txtSXAInstallDir.Size = new System.Drawing.Size(872, 31);
            this.txtSXAInstallDir.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 27);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 25);
            this.label4.TabIndex = 0;
            this.label4.Text = "SiteInstallDir:";
            // 
            // tabPgSitecore
            // 
            this.tabPgSitecore.Controls.Add(this.label16);
            this.tabPgSitecore.Controls.Add(this.txtSitecoreUserPassword);
            this.tabPgSitecore.Controls.Add(this.txtSitecoreUsername);
            this.tabPgSitecore.Controls.Add(this.label15);
            this.tabPgSitecore.Location = new System.Drawing.Point(8, 39);
            this.tabPgSitecore.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tabPgSitecore.Name = "tabPgSitecore";
            this.tabPgSitecore.Size = new System.Drawing.Size(1158, 349);
            this.tabPgSitecore.TabIndex = 4;
            this.tabPgSitecore.Text = "Sitecore";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(-3, 66);
            this.label16.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(236, 25);
            this.label16.TabIndex = 0;
            this.label16.Text = "SitecoreUserPassword:";
            // 
            // txtSitecoreUserPassword
            // 
            this.txtSitecoreUserPassword.Enabled = false;
            this.txtSitecoreUserPassword.Location = new System.Drawing.Point(256, 62);
            this.txtSitecoreUserPassword.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtSitecoreUserPassword.MaxLength = 15;
            this.txtSitecoreUserPassword.Name = "txtSitecoreUserPassword";
            this.txtSitecoreUserPassword.Size = new System.Drawing.Size(873, 31);
            this.txtSitecoreUserPassword.TabIndex = 18;
            this.txtSitecoreUserPassword.Text = "b";
            this.txtSitecoreUserPassword.UseSystemPasswordChar = true;
            // 
            // txtSitecoreUsername
            // 
            this.txtSitecoreUsername.Enabled = false;
            this.txtSitecoreUsername.Location = new System.Drawing.Point(256, 21);
            this.txtSitecoreUsername.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtSitecoreUsername.MaxLength = 15;
            this.txtSitecoreUsername.Name = "txtSitecoreUsername";
            this.txtSitecoreUsername.Size = new System.Drawing.Size(873, 31);
            this.txtSitecoreUsername.TabIndex = 17;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(-3, 24);
            this.label15.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(198, 25);
            this.label15.TabIndex = 0;
            this.label15.Text = "SitecoreUserName:";
            // 
            // tabPgSolr
            // 
            this.tabPgSolr.Controls.Add(this.txtSolrVersion);
            this.tabPgSolr.Controls.Add(this.label11);
            this.tabPgSolr.Controls.Add(this.label20);
            this.tabPgSolr.Controls.Add(this.txtSolrService);
            this.tabPgSolr.Controls.Add(this.label19);
            this.tabPgSolr.Controls.Add(this.txtSolrRoot);
            this.tabPgSolr.Controls.Add(this.txtSolrUrl);
            this.tabPgSolr.Controls.Add(this.label18);
            this.tabPgSolr.Location = new System.Drawing.Point(8, 39);
            this.tabPgSolr.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tabPgSolr.Name = "tabPgSolr";
            this.tabPgSolr.Size = new System.Drawing.Size(1158, 349);
            this.tabPgSolr.TabIndex = 5;
            this.tabPgSolr.Text = "Solr";
            // 
            // txtSolrVersion
            // 
            this.txtSolrVersion.Location = new System.Drawing.Point(273, 143);
            this.txtSolrVersion.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtSolrVersion.MaxLength = 50;
            this.txtSolrVersion.Name = "txtSolrVersion";
            this.txtSolrVersion.Size = new System.Drawing.Size(871, 31);
            this.txtSolrVersion.TabIndex = 24;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(15, 146);
            this.label11.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(129, 25);
            this.label11.TabIndex = 23;
            this.label11.Text = "SolrVersion:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(15, 101);
            this.label20.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(128, 25);
            this.label20.TabIndex = 0;
            this.label20.Text = "SolrService:";
            // 
            // txtSolrService
            // 
            this.txtSolrService.Location = new System.Drawing.Point(273, 98);
            this.txtSolrService.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtSolrService.MaxLength = 50;
            this.txtSolrService.Name = "txtSolrService";
            this.txtSolrService.Size = new System.Drawing.Size(871, 31);
            this.txtSolrService.TabIndex = 22;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(15, 60);
            this.label19.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(101, 25);
            this.label19.TabIndex = 0;
            this.label19.Text = "SolrRoot:";
            // 
            // txtSolrRoot
            // 
            this.txtSolrRoot.Location = new System.Drawing.Point(273, 56);
            this.txtSolrRoot.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtSolrRoot.MaxLength = 100;
            this.txtSolrRoot.Name = "txtSolrRoot";
            this.txtSolrRoot.Size = new System.Drawing.Size(871, 31);
            this.txtSolrRoot.TabIndex = 21;
            // 
            // txtSolrUrl
            // 
            this.txtSolrUrl.Location = new System.Drawing.Point(273, 15);
            this.txtSolrUrl.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtSolrUrl.MaxLength = 100;
            this.txtSolrUrl.Name = "txtSolrUrl";
            this.txtSolrUrl.Size = new System.Drawing.Size(871, 31);
            this.txtSolrUrl.TabIndex = 20;
            this.toolTipSIF.SetToolTip(this.txtSolrUrl, "Enter Solr Url and press Solr Check button to populate other fields");
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(15, 20);
            this.label18.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(83, 25);
            this.label18.TabIndex = 0;
            this.label18.Text = "SolrUrl:";
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblStatus.Location = new System.Drawing.Point(28, 748);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(1564, 38);
            this.lblStatus.TabIndex = 76;
            this.lblStatus.Text = "Happy Sitecoring!";
            // 
            // btnInstall
            // 
            this.btnInstall.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnInstall.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInstall.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnInstall.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnInstall.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnInstall.Location = new System.Drawing.Point(1405, 696);
            this.btnInstall.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(180, 48);
            this.btnInstall.TabIndex = 74;
            this.btnInstall.Text = "Install";
            this.toolTipSIF.SetToolTip(this.btnInstall, "Launches the Install Script in Powershell Command");
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
            this.btnGenerate.Location = new System.Drawing.Point(1144, 696);
            this.btnGenerate.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(251, 48);
            this.btnGenerate.TabIndex = 73;
            this.btnGenerate.Text = "Generate Scripts";
            this.toolTipSIF.SetToolTip(this.btnGenerate, "Generates and Saves Install and Uninstall Scripts for a site ");
            this.btnGenerate.UseVisualStyleBackColor = false;
            this.btnGenerate.EnabledChanged += new System.EventHandler(this.btnGenerate_EnabledChanged);
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // HorizonInstallerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1613, 794);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnInstall);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlDetails);
            this.Controls.Add(this.btnScriptPreview);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.lblStepInfo);
            this.Controls.Add(this.btnFirst);
            this.Controls.Add(this.btnLast);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnValidateAll);
            this.Controls.Add(this.btnSolr);
            this.Controls.Add(this.btnDbConn);
            this.Controls.Add(this.btnAppSettings);
            this.Controls.Add(this.btnPrerequisites);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "HorizonInstallerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SCIA - Horizon Installer";
            this.panel2.ResumeLayout(false);
            this.pnlDetails.ResumeLayout(false);
            this.pnlDetails.PerformLayout();
            this.tabDetails.ResumeLayout(false);
            this.tabPgDBConnection.ResumeLayout(false);
            this.tabPgDBConnection.PerformLayout();
            this.tabpgSiteInfo.ResumeLayout(false);
            this.tabpgSiteInfo.PerformLayout();
            this.tabPgGeneral.ResumeLayout(false);
            this.tabPgGeneral.PerformLayout();
            this.tabPgInstallDetails.ResumeLayout(false);
            this.tabPgInstallDetails.PerformLayout();
            this.tabPgSitecore.ResumeLayout(false);
            this.tabPgSitecore.PerformLayout();
            this.tabPgSolr.ResumeLayout(false);
            this.tabPgSolr.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnScriptPreview;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Label lblStepInfo;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnValidateAll;
        private System.Windows.Forms.Button btnSolr;
        private System.Windows.Forms.Button btnDbConn;
        private System.Windows.Forms.Button btnAppSettings;
        private System.Windows.Forms.Button btnPrerequisites;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckedListBox chkStepsList;
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
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox txtSiteNameSuffix;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtSiteNamePrefix;
        private System.Windows.Forms.TextBox txtSiteName;
        private System.Windows.Forms.TabPage tabPgGeneral;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSitecoreIdentityServerUrl;
        private System.Windows.Forms.TextBox txtIDServerSiteName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPgInstallDetails;
        private System.Windows.Forms.TextBox txtHorizonInstallDir;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSXAInstallDir;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage tabPgSitecore;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtSitecoreUserPassword;
        private System.Windows.Forms.TextBox txtSitecoreUsername;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TabPage tabPgSolr;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtSolrService;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtSolrRoot;
        private System.Windows.Forms.TextBox txtSolrUrl;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.TextBox txtSolrVersion;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ToolTip toolTipSIF;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtHorizonAppUrl;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtHorizonInstanceName;
        private System.Windows.Forms.TextBox txtIDServerAppPoolName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtIDServerInstallDir;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtSiteUrl;
    }
}