namespace SCIA
{
    partial class ClearDB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClearDB));
            this.label3 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtNameString = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label25 = new System.Windows.Forms.Label();
            this.txtSolrVersion = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbDBPrefixName = new System.Windows.Forms.RadioButton();
            this.rbDBName = new System.Windows.Forms.RadioButton();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 204);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(368, 25);
            this.label3.TabIndex = 96;
            this.label3.Text = "Provide DB Prefix / Name to delete....";
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblStatus.Location = new System.Drawing.Point(31, 635);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(1122, 38);
            this.lblStatus.TabIndex = 95;
            this.lblStatus.Text = "Happy Sitecoring!";
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDelete.Location = new System.Drawing.Point(962, 566);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(180, 48);
            this.btnDelete.TabIndex = 94;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Location = new System.Drawing.Point(26, 253);
            this.panel2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1122, 287);
            this.panel2.TabIndex = 97;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Controls.Add(this.txtNameString);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(9, 28);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1053, 198);
            this.panel3.TabIndex = 0;
            // 
            // txtNameString
            // 
            this.txtNameString.Location = new System.Drawing.Point(198, 113);
            this.txtNameString.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtNameString.MaxLength = 100;
            this.txtNameString.Name = "txtNameString";
            this.txtNameString.Size = new System.Drawing.Size(827, 31);
            this.txtNameString.TabIndex = 88;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 25);
            this.label2.TabIndex = 87;
            this.label2.Text = "NameString:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1.Location = new System.Drawing.Point(25, 27);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(158, 154);
            this.panel1.TabIndex = 92;
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label25.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label25.Location = new System.Drawing.Point(198, 27);
            this.label25.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(950, 154);
            this.label25.TabIndex = 91;
            this.label25.Text = "Delete DB Details";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtSolrVersion
            // 
            this.txtSolrVersion.Location = new System.Drawing.Point(235, 301);
            this.txtSolrVersion.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtSolrVersion.MaxLength = 100;
            this.txtSolrVersion.Name = "txtSolrVersion";
            this.txtSolrVersion.Size = new System.Drawing.Size(827, 31);
            this.txtSolrVersion.TabIndex = 93;
            this.txtSolrVersion.Text = "8.4.0";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbDBName);
            this.groupBox1.Controls.Add(this.rbDBPrefixName);
            this.groupBox1.Location = new System.Drawing.Point(20, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1005, 82);
            this.groupBox1.TabIndex = 89;
            this.groupBox1.TabStop = false;
            // 
            // rbDBPrefixName
            // 
            this.rbDBPrefixName.AutoSize = true;
            this.rbDBPrefixName.Checked = true;
            this.rbDBPrefixName.Location = new System.Drawing.Point(216, 30);
            this.rbDBPrefixName.Name = "rbDBPrefixName";
            this.rbDBPrefixName.Size = new System.Drawing.Size(195, 29);
            this.rbDBPrefixName.TabIndex = 0;
            this.rbDBPrefixName.TabStop = true;
            this.rbDBPrefixName.Text = "DB Prefix Name";
            this.rbDBPrefixName.UseVisualStyleBackColor = true;
            // 
            // rbDBName
            // 
            this.rbDBName.AutoSize = true;
            this.rbDBName.Location = new System.Drawing.Point(634, 30);
            this.rbDBName.Name = "rbDBName";
            this.rbDBName.Size = new System.Drawing.Size(134, 29);
            this.rbDBName.TabIndex = 1;
            this.rbDBName.Text = "DB Name";
            this.rbDBName.UseVisualStyleBackColor = true;
            // 
            // ClearDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1175, 693);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.txtSolrVersion);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ClearDB";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SCIA - Delete DB ";
            this.Load += new System.EventHandler(this.ClearDB_Load);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtNameString;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox txtSolrVersion;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbDBName;
        private System.Windows.Forms.RadioButton rbDBPrefixName;
    }
}