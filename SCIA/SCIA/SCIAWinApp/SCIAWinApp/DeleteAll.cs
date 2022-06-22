using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace SCIA
{
    public partial class DeleteAll : Form
    {
        readonly string FileName = string.Empty;
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
