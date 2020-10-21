using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SCIA
{
    public partial class DeleteAll : Form
    {
        string FileName = string.Empty;
        public DeleteAll(string fileName)
        {
            InitializeComponent();
            FileName = fileName;
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            LaunchPSScript(FileName);
            lblStatus.Text = "Delete launched successfully through Powershell....";
            lblStatus.ForeColor = Color.DarkGreen;
        }

        void LaunchPSScript(string scriptname)
        {
            var script = @".\" + scriptname;
            var startInfo = new ProcessStartInfo()
            {
                FileName = "powershell.exe",
                Arguments = $"-NoProfile -noexit -ExecutionPolicy unrestricted \"{script}\"",
                UseShellExecute = false
            };
            Process.Start(startInfo);
        }
    }
}
