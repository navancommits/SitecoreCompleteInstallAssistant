namespace SCIA
{
    partial class mdiSitecoreComplete
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mdiSitecoreComplete));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.sitecoreMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.siaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sitecoreContainerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commerceMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.sitecoreCommerceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commerceContainerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.siaToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.sifltoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.sitecoreContainerToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.sitecoreCommerceToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.sitecoreCommerceContainerToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.sdnLogintoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.sifStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sitecoreMenu,
            this.commerceMenu,
            this.helpMenu});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1264, 42);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // sitecoreMenu
            // 
            this.sitecoreMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.siaToolStripMenuItem,
            this.sifStripMenuItem,
            this.sitecoreContainerToolStripMenuItem,
            this.toolStripSeparator3,
            this.exitToolStripMenuItem});
            this.sitecoreMenu.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder;
            this.sitecoreMenu.Name = "sitecoreMenu";
            this.sitecoreMenu.Size = new System.Drawing.Size(121, 36);
            this.sitecoreMenu.Text = "&Sitecore";
            // 
            // siaToolStripMenuItem
            // 
            this.siaToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("siaToolStripMenuItem.Image")));
            this.siaToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.siaToolStripMenuItem.Name = "siaToolStripMenuItem";
            this.siaToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.siaToolStripMenuItem.Size = new System.Drawing.Size(560, 44);
            this.siaToolStripMenuItem.Text = "&Sitecore Install Assistant (SIA)";
            this.siaToolStripMenuItem.Click += new System.EventHandler(this.siaToolStripMenuItem_Click);
            // 
            // sitecoreContainerToolStripMenuItem
            // 
            this.sitecoreContainerToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("sitecoreContainerToolStripMenuItem.Image")));
            this.sitecoreContainerToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.sitecoreContainerToolStripMenuItem.Name = "sitecoreContainerToolStripMenuItem";
            this.sitecoreContainerToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.sitecoreContainerToolStripMenuItem.Size = new System.Drawing.Size(560, 44);
            this.sitecoreContainerToolStripMenuItem.Text = "Sitecore &Container Installer";
            this.sitecoreContainerToolStripMenuItem.Click += new System.EventHandler(this.OpenFile);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(557, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(560, 44);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolsStripMenuItem_Click);
            // 
            // commerceMenu
            // 
            this.commerceMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sitecoreCommerceToolStripMenuItem,
            this.commerceContainerToolStripMenuItem,
            this.toolStripSeparator6});
            this.commerceMenu.Name = "commerceMenu";
            this.commerceMenu.Size = new System.Drawing.Size(244, 36);
            this.commerceMenu.Text = "Sitecore Com&merce";
            // 
            // sitecoreCommerceToolStripMenuItem
            // 
            this.sitecoreCommerceToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("sitecoreCommerceToolStripMenuItem.Image")));
            this.sitecoreCommerceToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.sitecoreCommerceToolStripMenuItem.Name = "sitecoreCommerceToolStripMenuItem";
            this.sitecoreCommerceToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.sitecoreCommerceToolStripMenuItem.Size = new System.Drawing.Size(645, 44);
            this.sitecoreCommerceToolStripMenuItem.Text = "&Sitecore Commerce Install Assistant ";
            this.sitecoreCommerceToolStripMenuItem.Click += new System.EventHandler(this.sitecoreCommerceToolStripMenuItem_Click);
            // 
            // commerceContainerToolStripMenuItem
            // 
            this.commerceContainerToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("commerceContainerToolStripMenuItem.Image")));
            this.commerceContainerToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.commerceContainerToolStripMenuItem.Name = "commerceContainerToolStripMenuItem";
            this.commerceContainerToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.commerceContainerToolStripMenuItem.Size = new System.Drawing.Size(645, 44);
            this.commerceContainerToolStripMenuItem.Text = "Sitecore Commerce Contai&ner Installer";
            this.commerceContainerToolStripMenuItem.Click += new System.EventHandler(this.commerceContainerToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(642, 6);
            // 
            // helpMenu
            // 
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentsToolStripMenuItem,
            this.toolStripSeparator8});
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(85, 36);
            this.helpMenu.Text = "&Help";
            // 
            // contentsToolStripMenuItem
            // 
            this.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
            this.contentsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F1)));
            this.contentsToolStripMenuItem.Size = new System.Drawing.Size(338, 44);
            this.contentsToolStripMenuItem.Text = "&Contents";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(335, 6);
            // 
            // toolStrip
            // 
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.siaToolStripButton,
            this.sifltoolStripButton,
            this.toolStripSeparator4,
            this.sitecoreContainerToolStripButton,
            this.toolStripSeparator1,
            this.sitecoreCommerceToolStripButton,
            this.toolStripSeparator5,
            this.sitecoreCommerceContainerToolStripButton,
            this.toolStripSeparator2,
            this.sdnLogintoolStripButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 42);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.toolStrip.Size = new System.Drawing.Size(1264, 42);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "ToolStrip";
            // 
            // siaToolStripButton
            // 
            this.siaToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.siaToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("siaToolStripButton.Image")));
            this.siaToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.siaToolStripButton.Name = "siaToolStripButton";
            this.siaToolStripButton.Size = new System.Drawing.Size(46, 36);
            this.siaToolStripButton.Text = "Sitecore Install Assistant (SIA)";
            this.siaToolStripButton.Click += new System.EventHandler(this.siaToolStripButton_Click);
            // 
            // sifltoolStripButton
            // 
            this.sifltoolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.sifltoolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("sifltoolStripButton.Image")));
            this.sifltoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sifltoolStripButton.Name = "sifltoolStripButton";
            this.sifltoolStripButton.Size = new System.Drawing.Size(46, 36);
            this.sifltoolStripButton.Text = "Sitecore Install Framework (SIF)";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 42);
            // 
            // sitecoreContainerToolStripButton
            // 
            this.sitecoreContainerToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.sitecoreContainerToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("sitecoreContainerToolStripButton.Image")));
            this.sitecoreContainerToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.sitecoreContainerToolStripButton.Name = "sitecoreContainerToolStripButton";
            this.sitecoreContainerToolStripButton.Size = new System.Drawing.Size(46, 36);
            this.sitecoreContainerToolStripButton.Text = "Sitecore Container Installer";
            this.sitecoreContainerToolStripButton.Click += new System.EventHandler(this.OpenFile);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 42);
            // 
            // sitecoreCommerceToolStripButton
            // 
            this.sitecoreCommerceToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.sitecoreCommerceToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("sitecoreCommerceToolStripButton.Image")));
            this.sitecoreCommerceToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.sitecoreCommerceToolStripButton.Name = "sitecoreCommerceToolStripButton";
            this.sitecoreCommerceToolStripButton.Size = new System.Drawing.Size(46, 36);
            this.sitecoreCommerceToolStripButton.Text = "Sitecore Commerce Install Assistant";
            this.sitecoreCommerceToolStripButton.Click += new System.EventHandler(this.sitecoreCommerceToolStripButton_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 42);
            // 
            // sitecoreCommerceContainerToolStripButton
            // 
            this.sitecoreCommerceContainerToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.sitecoreCommerceContainerToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("sitecoreCommerceContainerToolStripButton.Image")));
            this.sitecoreCommerceContainerToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.sitecoreCommerceContainerToolStripButton.Name = "sitecoreCommerceContainerToolStripButton";
            this.sitecoreCommerceContainerToolStripButton.Size = new System.Drawing.Size(46, 36);
            this.sitecoreCommerceContainerToolStripButton.Text = "Sitecore Commerce Container Installer";
            this.sitecoreCommerceContainerToolStripButton.Click += new System.EventHandler(this.sitecoreCommerceContainerToolStripButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 42);
            // 
            // sdnLogintoolStripButton
            // 
            this.sdnLogintoolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.sdnLogintoolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("sdnLogintoolStripButton.Image")));
            this.sdnLogintoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sdnLogintoolStripButton.Name = "sdnLogintoolStripButton";
            this.sdnLogintoolStripButton.Size = new System.Drawing.Size(46, 36);
            this.sdnLogintoolStripButton.Text = "SDN Login";
            this.sdnLogintoolStripButton.ToolTipText = "SDN Login";
            this.sdnLogintoolStripButton.Click += new System.EventHandler(this.sdnLogintoolStripButton_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 829);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(2, 0, 28, 0);
            this.statusStrip.Size = new System.Drawing.Size(1264, 42);
            this.statusStrip.TabIndex = 2;
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(79, 32);
            this.toolStripStatusLabel.Text = "Status";
            // 
            // sifStripMenuItem
            // 
            this.sifStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("sifStripMenuItem.Image")));
            this.sifStripMenuItem.Name = "sifStripMenuItem";
            this.sifStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.sifStripMenuItem.Size = new System.Drawing.Size(560, 44);
            this.sifStripMenuItem.Text = "Sitecore Install Framework (SIF)";
            // 
            // mdiSitecoreComplete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 871);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "mdiSitecoreComplete";
            this.Text = "Sitecore Complete Install Assistant";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.mdiSitecoreComplete_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem sitecoreMenu;
        private System.Windows.Forms.ToolStripMenuItem siaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sitecoreContainerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem commerceMenu;
        private System.Windows.Forms.ToolStripMenuItem sitecoreCommerceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem commerceContainerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolStripMenuItem contentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton siaToolStripButton;
        private System.Windows.Forms.ToolStripButton sitecoreContainerToolStripButton;
        private System.Windows.Forms.ToolStripButton sitecoreCommerceToolStripButton;
        private System.Windows.Forms.ToolStripButton sitecoreCommerceContainerToolStripButton;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton sdnLogintoolStripButton;
        private System.Windows.Forms.ToolStripButton sifltoolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem sifStripMenuItem;
    }
}



