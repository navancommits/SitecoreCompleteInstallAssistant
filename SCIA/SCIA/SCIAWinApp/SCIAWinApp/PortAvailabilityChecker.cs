using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCIA
{
    public partial class PortAvailabilityChecker : Form
    {
        public PortAvailabilityChecker()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

       

        private bool ValidatePortNumber(NumericUpDown control, string controlString)
        {
            bool Valid = true;
            if (control.Value < 1024)
            {
                lblStatus.Text = controlString + " must be between 1024 to 49151... ";
                lblStatus.ForeColor = Color.Red;
                control.Focus();
                Valid = false;
            }
            
            return Valid;
        }

        private bool PortInRange(string input)
        {
            var regex = @"^([1-9][0-9]{0,3}|[1-5][0-9]{4}|6[0-4][0-9]{3}|65[0-4][0-9]{2}|655[0-2][0-9]|6553[0-5])$";
            var match = Regex.Match(input, regex, RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                lblStatus.Text = "Port range must be between 1 and 65535... ";
                lblStatus.ForeColor = Color.Red;
                return false;
            }

            return true;
        }

        private bool PerformPortValidations()
        {
            //if (!ValidatePortNumber(txtPortNumber, "Port Number")) return false;
            if (!PortInRange(txtPortNumber.Text)) return false;

            if (CommonFunctions.PortInUse(Convert.ToInt32(txtPortNumber.Value)))
            {
                lblStatus.Text = "Port unavailable... " ; 
                lblStatus.ForeColor = Color.Red;
                return false;
            }
                
            return true;
        }


        private void btnInstall_Click(object sender, EventArgs e)
        {
            if (!PerformPortValidations()) return;

            lblStatus.Text = "The port number is available...";
            lblStatus.ForeColor = Color.DarkGreen;
        }
    }
}
