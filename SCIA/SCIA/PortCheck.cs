using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;

namespace SCIA
{
    public partial class PortCheck : Form
    {
        public PortCheck(int CommerceOpsSvcPort, int CommerceShopsSvcPort,int CommerceAuthSvcPort, int CommerceMinionsPort, int BizFxPort)
        {
            InitializeComponent();
            if (IsPortNotinUse(CommerceOpsSvcPort) && ValidatePortNumber(CommerceOpsSvcPort, "Commerce Ops Svc Port")) chkCommerceOpsPort.Checked=true;
            if (IsPortNotinUse(CommerceShopsSvcPort) && ValidatePortNumber(CommerceShopsSvcPort, "Commerce Shops Svc Port")) chkCommerceShopsPort.Checked = true;
            if (IsPortNotinUse(CommerceAuthSvcPort) && ValidatePortNumber(CommerceAuthSvcPort, "Commerce Auth Svc Port")) chkCommerceAuhPort.Checked = true;
            if (IsPortNotinUse(CommerceMinionsPort) && ValidatePortNumber(CommerceMinionsPort, "Commerce Minions Svc Port")) chkCommerceMinionsPort.Checked = true;
            if (IsPortNotinUse(BizFxPort) && ValidatePortNumber(BizFxPort, "BizFx Port Number")) chkBizFxPort.Checked = true;            
        }

        public static bool PortInUse(int port)
        {
            bool inUse = false;

            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();


            foreach (IPEndPoint endPoint in ipEndPoints)
            {
                if (endPoint.Port == port)
                {
                    inUse = true;
                    break;
                }
            }

            return inUse;
        }

        private bool IsPortNotinUse(int portNumber)
        {
            if (PortInUse(Convert.ToInt32(portNumber)))
            {
                lblStatus.Text = portNumber + " port in use... provide a different number...";
                lblStatus.ForeColor = Color.Red;                
                return false;
            }
            return true;
        }

        private bool ValidatePortNumber(int portNumber, string controlString)
        {
            bool Valid = true;
            if (portNumber < 1024)
            {
                lblStatus.Text = controlString + " must be between 1024 to 49151... ";
                lblStatus.ForeColor = Color.Red;
                Valid = false;
            }
            return Valid;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
