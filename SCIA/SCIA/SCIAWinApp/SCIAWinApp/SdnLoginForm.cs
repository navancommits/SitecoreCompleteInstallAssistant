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
    public partial class SdnLoginForm : Form
    {
        public SdnLoginForm()
        {
            InitializeComponent();
        }

        private void SetStatusMessage(string statusmsg, Color color)
        {
            lblStatus.ForeColor = color;
            lblStatus.Text = statusmsg;
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (string.IsNullOrWhiteSpace(txtUser.Text) || string.IsNullOrWhiteSpace(txtPass.Text))
            {
                SetStatusMessage("SDN User / Password needed", Color.Red);
                return;
            }

            Login.username = txtUser.Text;
            Login.password = txtPass.Text;
            Login.requestUrl = "https://dev.sitecore.net/api/authorization";
            SetStatusMessage("Processing....", Color.Orange);
            CommonFunctions.InvokeWebRequest();
            if (!Login.Success)
            {
                SetStatusMessage("Wrong SDN Login Credentials", Color.Red);                
                return;
            }

            Cursor.Current = Cursors.Default;
            this.Hide();
        }
    }
}
