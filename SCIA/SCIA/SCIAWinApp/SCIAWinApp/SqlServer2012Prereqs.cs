using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCIA
{
    public partial class SqlServer2012Prereqs : Form
    {
        public SqlServer2012Prereqs()
        {
            InitializeComponent();
            if (CommonFunctions.FileSystemEntryExists("SharedManagementObjects2012.msi",null))
            {
                chkSMOMsi.Checked = true;
                chkSMOMsi.BackColor = Color.LightGreen;
            }

            if (CommonFunctions.FileSystemEntryExists("SQLSysClrTypes2012.msi", null))
            {
                chkSqlSysClrTypesMsi.Checked = true;
                chkSqlSysClrTypesMsi.BackColor = Color.LightGreen;
            }
        }

        private void WriteMainFile(string filename, string url, string name)
        {
            using var file = new StreamWriter(filename);
            file.WriteLine("if (-not(Test-Path -Path '" + name + "' -PathType Leaf)) {");
            file.WriteLine(
                "\tInvoke-WebRequest -Uri \"" + url + "\"  -OutFile \"" + name + "\" -UseBasicParsing");
            file.WriteLine(
                "\tStart-Process -FilePath \"" + name + "\"");
            file.WriteLine("}");
            file.Dispose();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            WriteMainFile("WriteSMO2012.ps1", "https://download.microsoft.com/download/F/3/C/F3C64941-22A0-47E9-BC9B-1A19B4CA3E88/ENU/x64/SharedManagementObjects.msi", "SharedManagementObjects2012.msi");
            CommonFunctions.LaunchPSScript(".\\WriteSMO2012.ps1");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            WriteMainFile("WriteSysClrTypes2012.ps1", "https://download.microsoft.com/download/F/3/C/F3C64941-22A0-47E9-BC9B-1A19B4CA3E88/ENU/x64/SQLSysClrTypes.msi", "SQLSysClrTypes2012.msi");
            CommonFunctions.LaunchPSScript(".\\WriteSysClrTypes2012.ps1");
        }

        private void chkSitecoreSetup_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
