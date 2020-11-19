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
    public partial class ClearAll : Form
    {
        public ClearAll()
        {
            InitializeComponent();
        }

        private void SetStatusMessage(string statusmsg, Color color)
        {
            lblStatus.ForeColor = color;
            lblStatus.Text = statusmsg;
        }

        private void DeleteScript(string path)
        {

            using var file = new StreamWriter(path);
            file.WriteLine("Param(");
            file.WriteLine("\t[string]$SolrService = \"" + txtServiceName.Text + "\",");
            file.WriteLine("\t[string]$PathToSolr = \"" + txtSolrFolder.Text + "\"");
            file.WriteLine(")");
            file.WriteLine();

            file.WriteLine("Function Remove-Service{");
            file.WriteLine("\t[CmdletBinding()]");
            file.WriteLine("\tparam(");
            file.WriteLine("\t\t[string]$serviceName");
            file.WriteLine("\t)");
            file.WriteLine("\tif(Get-Service $serviceName -ErrorAction SilentlyContinue){");
            file.WriteLine("\t\tsc.exe delete $serviceName -Force");
            file.WriteLine("\t}");
            file.WriteLine("}");
            file.WriteLine();

            file.WriteLine("Function Stop-Service{");
            file.WriteLine("\t[CmdletBinding()]");
            file.WriteLine("\tparam(");
            file.WriteLine("\t\t[string]$serviceName");
            file.WriteLine("\t)");
            file.WriteLine("\tif(Get-Service $serviceName -ErrorAction SilentlyContinue){");
            file.WriteLine("\t\tsc.exe stop $serviceName -Force");
            file.WriteLine("\t}");
            file.WriteLine("}");
            file.WriteLine();

            file.WriteLine("Write-Host \"Stopping solr service\"");
            file.WriteLine("Stop-Service $SolrService");
            file.WriteLine("Write-Host \"Solr service stopped successfully\"");
            file.WriteLine();
            
            file.WriteLine("Write-Host \"Removing Services\"");
            file.WriteLine("Remove-Service $SolrService");
            
            file.WriteLine("Write-Host \"Services removed\"");
            file.WriteLine();

            file.WriteLine("# Delete solr cores");
            file.WriteLine("Write-Host \"Deleting Solr directory\"");
            file.WriteLine("$pathToCores = $PathToSolr");
            file.WriteLine("rm $PathToSolr -recurse -force -ea ig");
            file.WriteLine("Write-Host \"Solr folder deleted successfully\"");
        
            file.WriteLine("pop-location");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtServiceName.Text)) { SetStatusMessage("Service Name required....", Color.Red); return; }

            DeleteScript(SCIASettings.FilePrefixAppString + txtServiceName.Text + "_Delete_Service_Script.ps1");
            DeleteAll deleteAll = new DeleteAll(SCIASettings.FilePrefixAppString + txtServiceName.Text + "_Delete_Service_Script.ps1");
            deleteAll.ShowDialog();
        }
    }
}
