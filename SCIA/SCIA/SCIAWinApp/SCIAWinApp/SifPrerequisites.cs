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
    public partial class SifPrerequisites : Form
    {
        bool AllChecked = true;
        List<VersionPrerequisites> prereqs;
        string destFolder = string.Empty;
        DBServerDetails dbServer;
        const string zipType = "sitecoredevsetup";
        string version = "10.0";
        bool xpoFile = true;
        ZipVersions zipVersions = null;
        public SifPrerequisites(DBServerDetails dbServerDetails)
        {
            InitializeComponent();
            dbServer = dbServerDetails;
            CommonFunctions.ConnectionString = CommonFunctions.BuildConnectionString(dbServer.Server, "SCIA_DB", dbServer.Username, dbServer.Password);
            version = Version.SitecoreVersion;
            this.Text = this.Text + " for Sitecore v" + version;


            switch (Version.SitecoreVersion)
            {
                case "10.3.0":
                case "10.2.0":
                case "10.1.1":
                case "10.1.0":
                case "10.0":
                case "10.0.1":
                case "9.3":
                case "9.2":
                    destFolder = CommonFunctions.GetZipNamefromWdpVersion("sitecoredevsetup", Version.SitecoreVersion);
                    zipVersions = CommonFunctions.GetZipVersionData(Version.SitecoreVersion, "sitecoredevsetup");
                    prereqs = CommonFunctions.GetVersionPrerequisites(version, "sitecoredevsetup");
                    xpoFile = false;
                    break;
                case "9.1.1":
                case "9.0":
                case "9.0.1":
                case "9.0.2":
                    destFolder = CommonFunctions.GetZipNamefromWdpVersion("sitecoresif", Version.SitecoreVersion);
                    zipVersions = CommonFunctions.GetZipVersionData(Version.SitecoreVersion, "sitecoresif");
                    prereqs = CommonFunctions.GetVersionPrerequisites(version, "sitecoresif");
                    break;
                default:
                    break;
            }

            chkSitecoreSetup.Text = destFolder + " Folder";
            CheckPrerequisites();
            if (AllChecked)
            {
                lblStatus.ForeColor = Color.DarkGreen;
                lblStatus.Text = "All Prerequisites Available";
            }
            else
            {
                lblStatus.ForeColor = Color.Red;
                lblStatus.Text = "One or more missing Prerequisites....";
            }
        }

        private void CheckPrerequisites()
        {

            if (!CommonFunctions.FileSystemEntryExists(destFolder, null, "folder", false)) { AllChecked = false; return; }
            if (!CommonFunctions.FileSystemEntryExists("license.xml",null, "file")) { AllChecked = false; return; }
            chkSitecoreSetup.Checked = true;
            chkSitecoreSetup.BackColor = Color.LightGreen;
            chkLicense.Checked = true;
            chkLicense.BackColor = Color.LightGreen;
        }


        private void SetStatusMessage(string statusmsg, Color color)
        {
            lblStatus.ForeColor = color;
            lblStatus.Text = statusmsg;
        }

        void WritePrerequisitesFile(string path)
        {
            using var file = new StreamWriter(path);
            file.WriteLine("{");
            file.WriteLine("    \"Parameters\": {");
            file.WriteLine("        \"TempLocation\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Description\": \"Alternative location to save downloads. If left on the default $Env:Temp will be used.\",");
            file.WriteLine("            \"DefaultValue\": \"\",");
            file.WriteLine("            \"Validate\": \"[TestPath(variable('Temp.Location'))]\"");
            file.WriteLine("        },");
            file.WriteLine("        \"PSRepositoryPSGallery\" : {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Description\": \"Default PS Repository\",");
            file.WriteLine("            \"DefaultValue\": \"PSGallery\"");
            file.WriteLine("        },");
            file.WriteLine("        \"WebPlatformDownload\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Description\": \"Download location of Microsoft Web Platform Installer 5.0\",");
            file.WriteLine("            \"DefaultValue\": \"https://download.microsoft.com/download/C/F/F/CFF3A0B8-99D4-41A2-AE1A-496C08BEB904/WebPlatformInstaller_amd64_en-US.msi\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SQLClrTypesx86Download\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Description\": \"Download location of SQL CLR Types x86\",");
            file.WriteLine("            \"DefaultValue\": \"https://download.microsoft.com/download/C/1/9/C1917410-8976-4AE0-98BF-1104349EA1E6/x86/SQLSysClrTypes.msi\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SQLClrTypesx64Download\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Description\": \"Download location of SQL CLR Types x64\",");
            file.WriteLine("            \"DefaultValue\": \"https://download.microsoft.com/download/C/1/9/C1917410-8976-4AE0-98BF-1104349EA1E6/x64/SQLSysClrTypes.msi\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SQLDacFrameworkx86Download\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Description\": \"Download location of SQL DAC Framework x86\",");
            file.WriteLine("            \"DefaultValue\": \"https://download.microsoft.com/download/6/E/4/6E406E38-0A01-4DD1-B85E-6CA7CF79C8F7/EN/x86/DacFramework.msi\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SQLDacFrameworkx64Download\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Description\": \"Download location of SQL DAC Framework x64\",");
            file.WriteLine("            \"DefaultValue\": \"https://download.microsoft.com/download/6/E/4/6E406E38-0A01-4DD1-B85E-6CA7CF79C8F7/EN/x64/DacFramework.msi\"");
            file.WriteLine("        },");
            file.WriteLine("        \"VisualC++2015x86Download\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Description\": \"Download location of Visual C++ 2015 x86\",");
            file.WriteLine("            \"DefaultValue\": \"https://download.microsoft.com/download/6/D/F/6DF3FF94-F7F9-4F0B-838C-A328D1A7D0EE/vc_redist.x86.exe\"");
            file.WriteLine("        },");
            file.WriteLine("        \"VisualC++2015x64Download\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Description\": \"Download location of Visual C++ 2015 x64\",");
            file.WriteLine("            \"DefaultValue\": \"https://download.microsoft.com/download/6/D/F/6DF3FF94-F7F9-4F0B-838C-A328D1A7D0EE/vc_redist.x64.exe\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SQLODBCDriversx64\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Description\": \"Download location of SQL ODBC Drivers 13.1\",");
            file.WriteLine("            \"DefaultValue\" : \"https://download.microsoft.com/download/D/5/E/D5EEF288-A277-45C8-855B-8E2CB7E25B96/x64/msodbcsql.msi\"");
            file.WriteLine("        },");
            file.WriteLine("        \"DotNetFrameworkDownload\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Description\": \"Download location of .net framework\",");
            file.WriteLine("            \"DefaultValue\": \"https://download.visualstudio.microsoft.com/download/pr/014120d7-d689-4305-befd-3cb711108212/0fd66638cde16859462a6243a4629a50/ndp48-x86-x64-allos-enu.exe\"");
            file.WriteLine("        },");
            file.WriteLine("        \"DotNetHostingDownload\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Description\": \"Download location of .net core 2.1.x Hosting Bundle\",");
            file.WriteLine("            \"DefaultValue\": \"https://download.visualstudio.microsoft.com/download/pr/3e3c37fb-4d77-4558-a78c-17434e1cc804/60116643f610fb43f858af4e0dc1b223/dotnet-hosting-2.1.23-win.exe\"");
            file.WriteLine("        },");
            file.WriteLine("        \"DotNet4RegistryLocation\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Description\": \"Registry location of .net4 release key\",");
            file.WriteLine("            \"DefaultValue\": \"HKLM:SOFTWARE\\\\Microsoft\\\\NET Framework Setup\\\\NDP\\\\v4\\\\Full\"");
            file.WriteLine("        },");
            file.WriteLine("        \"ComponentBasedServicing\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Description\": \"Registry location of component based servicing reboot pending key.\",");
            file.WriteLine("            \"DefaultValue\": \"HKLM:SOFTWARE\\\\Microsoft\\\\Windows\\\\CurrentVersion\\\\Component Based Servicing\\\\RebootPending\"");
            file.WriteLine("        }");
            file.WriteLine("    },");
            file.WriteLine("    \"Variables\": {");
            file.WriteLine("        \"SQLServer.Module\": \"[GetModule(ListAvailable:True,Name:'SqlServer',ErrorAction:'SilentlyContinue')]\",");
            file.WriteLine("        \"SQLServer.Module.First\": \"[variable('SqlServer.Module')[0]]\",");
            file.WriteLine("        \"SQLServer.Module.Test\": \"[if(variable('SQLServer.Module'),variable('SQLServer.Module.First'),$null)]\",");
            file.WriteLine("        \"SQLServer.Module.Version\": \"[SelectObject(InputObject:variable('SQLServer.Module.Test'),ExpandProperty:'Version')]\",");
            file.WriteLine("        \"SQLServer.Module.Version.Compare\": \"[InvokeExpression(Command:variable('SQLServer.Module.Version.Command'))]\",");
            file.WriteLine("        \"SQLServer.Module.Version.Command\": \"[concat('[system.version]\\\"',variable('SQLServer.Module.Null.Version'),'\\\" -ge [system.version]\\\"',variable('SQLServer.Module.Minimum.Version'),'\\\"')]\",");
            file.WriteLine("        \"SQLServer.Module.Null.Version\": \"[if(variable('SQLServer.Module.Version'),variable('SQLServer.Module.Version'),'0.0')]\",");
            file.WriteLine("        \"WebPlatform.Package\": \"[GetPackage(Name:'Microsoft` Web` Platform` Installer*',ErrorAction:'SilentlyContinue')]\",");
            file.WriteLine("        \"WebPlatform.Version\": \"[SelectObject(InputObject:variable('WebPlatform.Package'),ExpandProperty:'Version')]\",");
            file.WriteLine("        \"WebPlatform.Version.Compare\": \"[InvokeExpression(Command:variable('WebPlatform.Version.Command'))]\",");
            file.WriteLine("        \"WebPlatform.Version.Command\": \"[concat('[system.version]\\\"',variable('WebPlatform.Null.Version'),'\\\" -ge [system.version]\\\"',variable('WebPlatform.Minimum.Version'),'\\\"')]\",");
            file.WriteLine("        \"WebPlatform.Null.Version\": \"[if(variable('WebPlatform.Version'),variable('WebPlatform.Version'),'0.0')]\",");
            file.WriteLine("        \"WebDeploy.Package\": \"[GetPackage(Name:'Microsoft` Web` Deploy*',ErrorAction:'SilentlyContinue')]\",");
            file.WriteLine("        \"WebDeploy.Version\": \"[SelectObject(InputObject:variable('WebDeploy.Package'),ExpandProperty:'Version')]\",");
            file.WriteLine("        \"WebDeploy.Version.Compare\": \"[InvokeExpression(Command:variable('WebDeploy.Version.Command'))]\",");
            file.WriteLine("        \"WebDeploy.Version.Command\": \"[concat('[system.version]\\\"',variable('WebDeploy.Null.Version'),'\\\" -ge [system.version]\\\"',variable('WebDeploy.Minimum.Version'),'\\\"')]\",");
            file.WriteLine("        \"WebDeploy.Null.Version\": \"[if(variable('WebDeploy.Version'),variable('WebDeploy.Version'),'0.0')]\",");
            file.WriteLine("        \"IIS.URLRewrite.Package\": \"[GetPackage(Name:'IIS` URL` Rewrite` Module` 2',ErrorAction:'SilentlyContinue')]\",");
            file.WriteLine("        \"IIS.URLRewrite.Version\": \"[SelectObject(InputObject:variable('IIS.URLRewrite.Package'),ExpandProperty:'Version')]\",");
            file.WriteLine("        \"IIS.URLRewrite.Version.Compare\": \"[InvokeExpression(Command:variable('IIS.URLRewrite.Version.Command'))]\",");
            file.WriteLine("        \"IIS.URLRewrite.Version.Command\": \"[concat('[system.version]\\\"',variable('IIS.URLRewrite.Null.Version'),'\\\" -ge [system.version]\\\"',variable('IIS.URLRewrite.Minimum.Version'),'\\\"')]\",");
            file.WriteLine("        \"IIS.URLRewrite.Null.Version\": \"[if(variable('IIS.URLRewrite.Version'),variable('IIS.URLRewrite.Version'),'0.0')]\",");
            file.WriteLine("        \"SQL.CLR.Types.Package\": \"[GetPackage(Name:'Microsoft` System` CLR` Types` for` SQL` Server` 2017',ErrorAction:'SilentlyContinue')]\",");
            file.WriteLine("        \"SQL.CLR.Types.Version\": \"[SelectObject(InputObject:variable('SQL.CLR.Types.Package'),ExpandProperty:'Version')]\",");
            file.WriteLine("        \"SQL.CLR.Types.Version.Compare\": \"[InvokeExpression(Command:variable('SQL.CLR.Types.Version.Command'))]\",");
            file.WriteLine("        \"SQL.CLR.Types.Version.Command\": \"[concat('[system.version]\\\"',variable('SQL.CLR.Types.Null.Version'),'\\\" -ge [system.version]\\\"',variable('SQL.CLR.Types.Minimum.Version'),'\\\"')]\",");
            file.WriteLine("        \"SQL.CLR.Types.Null.Version\": \"[if(variable('SQL.CLR.Types.Version'),variable('SQL.CLR.Types.Version'),'0.0')]\",");
            file.WriteLine("        \"SQL.Dac.Framework.x86.Package\": \"[GetPackage(Name:'Microsoft` SQL` Server` Data-Tier` Application` Framework` `(x86`)',ErrorAction:'SilentlyContinue')]\",");
            file.WriteLine("        \"SQL.Dac.Framework.x86.Version\": \"[SelectObject(InputObject:variable('SQL.Dac.Framework.x86.Package'),ExpandProperty:'Version')]\",");
            file.WriteLine("        \"SQL.Dac.Framework.x86.Version.Compare\": \"[InvokeExpression(Command:variable('SQL.Dac.Framework.x86.Version.Command'))]\",");
            file.WriteLine("        \"SQL.Dac.Framework.x86.Version.Command\": \"[concat('[system.version]\\\"',variable('SQL.Dac.Framework.x86.Null.Version'),'\\\" -ge [system.version]\\\"',variable('SQL.Dac.Framework.x86.Minimum.Version'),'\\\"')]\",");
            file.WriteLine("        \"SQL.Dac.Framework.x86.Null.Version\": \"[if(variable('SQL.Dac.Framework.x86.Version'),variable('SQL.Dac.Framework.x86.Version'),'0.0')]\",");
            file.WriteLine("        \"SQL.Dac.Framework.x64.Package\": \"[GetPackage(Name:'Microsoft` SQL` Server` Data-Tier` Application` Framework` `(x64`)',ErrorAction:'SilentlyContinue')]\",");
            file.WriteLine("        \"SQL.Dac.Framework.x64.Version\": \"[SelectObject(InputObject:variable('SQL.Dac.Framework.x64.Package'),ExpandProperty:'Version')]\",");
            file.WriteLine("        \"SQL.Dac.Framework.x64.Version.Compare\": \"[InvokeExpression(Command:variable('SQL.Dac.Framework.x64.Version.Command'))]\",");
            file.WriteLine("        \"SQL.Dac.Framework.x64.Version.Command\": \"[concat('[system.version]\\\"',variable('SQL.Dac.Framework.x64.Null.Version'),'\\\" -ge [system.version]\\\"',variable('SQL.Dac.Framework.x64.Minimum.Version'),'\\\"')]\",");
            file.WriteLine("        \"SQL.Dac.Framework.x64.Null.Version\": \"[if(variable('SQL.Dac.Framework.x64.Version'),variable('SQL.Dac.Framework.x64.Version'),'0.0')]\",");
            file.WriteLine("        \"VisualC++.x86.Package\": \"[GetPackage(Name:'Microsoft` Visual` C++` 201*` Redistributable` `(x86`)` -` 14*',ErrorAction:'SilentlyContinue')]\",");
            file.WriteLine("        \"VisualC++.x86.Package.First\": \"[variable('VisualC++.x86.Package')[0]]\",");
            file.WriteLine("        \"VisualC++.x86.Package.Test\": \"[if(variable('VisualC++.x86.Package'),variable('VisualC++.x86.Package.First'),$null)]\",");
            file.WriteLine("        \"VisualC++.x86.Version\": \"[SelectObject(InputObject:variable('VisualC++.x86.Package.Test'),ExpandProperty:'Version')]\",");
            file.WriteLine("        \"VisualC++.x86.Version.Compare\": \"[InvokeExpression(Command:variable('VisualC++.x86.Version.Command'))]\",");
            file.WriteLine("        \"VisualC++.x86.Version.Command\": \"[concat('[system.version]\\\"',variable('VisualC++.x86.Null.Version'),'\\\" -ge [system.version]\\\"',variable('VisualC++.x86.Minimum.Version'),'\\\"')]\",");
            file.WriteLine("        \"VisualC++.x86.Null.Version\": \"[if(variable('VisualC++.x86.Version'),variable('VisualC++.x86.Version'),'0.0')]\",");
            file.WriteLine("        \"VisualC++.x86.Debug.Package\": \"[GetPackage(Name:'Microsoft` Visual` C++` 201*` x86` Debug` Runtime` -` 14*',ErrorAction:'SilentlyContinue'))]\",");
            file.WriteLine("        \"VisualC++.x86.Debug.Package.First\": \"[variable('VisualC++.x86.Debug.Package')[0]]\",");
            file.WriteLine("        \"VisualC++.x86.Debug.Package.Test\": \"[if(variable('VisualC++.x86.Debug.Package'),variable('VisualC++.x86.Debug.Package.First'),$null)]\",");
            file.WriteLine("        \"VisualC++.x86.Debug.Version\": \"[SelectObject(InputObject:variable('VisualC++.x86.Debug.Package'),ExpandProperty:'Version')]\",");
            file.WriteLine("        \"VisualC++.x86.Debug.Version.Compare\": \"[InvokeExpression(Command:variable('VisualC++.x86.Debug.Version.Command'))]\",");
            file.WriteLine("        \"VisualC++.x86.Debug.Version.Command\": \"[concat('[system.version]\\\"',variable('VisualC++.x86.Debug.Null.Version'),'\\\" -ge [system.version]\\\"',variable('VisualC++.x86.Debug.Minimum.Version'),'\\\"')]\",");
            file.WriteLine("        \"VisualC++.x86.Debug.Null.Version\": \"[if(variable('VisualC++.x86.Debug.Version'),variable('VisualC++.x86.Debug.Version'),'0.0')]\",");
            file.WriteLine("        \"VisualC++.x64.Package\": \"[GetPackage(Name:'Microsoft` Visual` C++` 201*` Redistributable` `(x64`)` -` 14*',ErrorAction:'SilentlyContinue')]\",");
            file.WriteLine("        \"VisualC++.x64.Package.First\": \"[variable('VisualC++.x64.Package')[0]]\",");
            file.WriteLine("        \"VisualC++.x64.Package.Test\": \"[if(variable('VisualC++.x64.Package'),variable('VisualC++.x64.Package.First'),$null)]\",");
            file.WriteLine("        \"VisualC++.x64.Version\": \"[SelectObject(InputObject:variable('VisualC++.x64.Package.Test'),ExpandProperty:'Version')]\",");
            file.WriteLine("        \"VisualC++.x64.Version.Compare\": \"[InvokeExpression(Command:variable('VisualC++.x64.Version.Command'))]\",");
            file.WriteLine("        \"VisualC++.x64.Version.Command\": \"[concat('[system.version]\\\"',variable('VisualC++.x64.Null.Version'),'\\\" -ge [system.version]\\\"',variable('VisualC++.x64.Minimum.Version'),'\\\"')]\",");
            file.WriteLine("        \"VisualC++.x64.Null.Version\": \"[if(variable('VisualC++.x64.Version'),variable('VisualC++.x64.Version'),'0.0')]\",");
            file.WriteLine("        \"VisualC++.x64.Debug.Package\": \"[GetPackage(Name:'Microsoft` Visual` C++` 201*` x64` Debug` Runtime` -` 14*',ErrorAction:'SilentlyContinue')]\",");
            file.WriteLine("        \"VisualC++.x64.Debug.Version\": \"[SelectObject(InputObject:variable('VisualC++.x64.Debug.Package'),ExpandProperty:'Version')]\",");
            file.WriteLine("        \"VisualC++.x64.Debug.Version.Compare\": \"[InvokeExpression(Command:variable('VisualC++.x64.Debug.Version.Command'))]\",");
            file.WriteLine("        \"VisualC++.x64.Debug.Version.Command\": \"[concat('[system.version]\\\"',variable('VisualC++.x64.Debug.Null.Version'),'\\\" -ge [system.version]\\\"',variable('VisualC++.x64.Debug.Minimum.Version'),'\\\"')]\",");
            file.WriteLine("        \"VisualC++.x64.Debug.Null.Version\": \"[if(variable('VisualC++.x64.Debug.Version'),variable('VisualC++.x64.Debug.Version'),'0.0')]\",");
            file.WriteLine("        \"SQL.ODBC.Package\": \"[GetPackage(Name:'Microsoft` ODBC` Driver` 13` for` SQL` Server',ErrorAction:'SilentlyContinue')]\",");
            file.WriteLine("        \"SQL.ODBC.Version\": \"[SelectObject(InputObject:variable('SQL.ODBC.Package'),ExpandProperty:'Version')]\",");
            file.WriteLine("        \"SQL.ODBC.Version.Compare\": \"[InvokeExpression(Command:variable('SQL.ODBC.Version.Command'))]\",");
            file.WriteLine("        \"SQL.ODBC.Version.Command\": \"[concat('[system.version]\\\"',variable('SQL.ODBC.Null.Version'),'\\\" -ge [system.version]\\\"',variable('SQL.ODBC.Minimum.Version'),'\\\"')]\",");
            file.WriteLine("        \"SQL.ODBC.Null.Version\": \"[if(variable('SQL.ODBC.Version'),variable('SQL.ODBC.Version'),'0.0')]\",");
            file.WriteLine("        \"Net.Hosting.Package\": \"[GetPackage(Name:'Microsoft` .NET` Core` 2.1.12` -` Windows` Server` Hosting',ErrorAction:'SilentlyContinue')]\",");
            file.WriteLine("        \"VisualC++.x86.Compare\": \"[or(variable('VisualC++.x86.Version.Compare'),variable('VisualC++.x86.Debug.Version.Compare'))]\",");
            file.WriteLine("        \"VisualC++.x64.Compare\": \"[or(variable('VisualC++.x64.Version.Compare'),variable('VisualC++.x64.Debug.Version.Compare'))]\",");
            file.WriteLine("        \"Check.DotNetFramework.Version\" : \"[ValidateRange(Min:0,Max:528039,Param:variable('Get.DotNet.Version'))]\",");
            file.WriteLine("        \"Check.Reboot.Pending\": \"[TestPath(Path:parameter('ComponentBasedServicing'))]\",");
            file.WriteLine("        \"IIS.URLRewrite.Minimum.Version\": \"7.2.1980\",");
            file.WriteLine("        \"SQL.CLR.Types.Minimum.Version\": \"14.0.1000.169\",");
            file.WriteLine("        \"SQL.Dac.Framework.x86.Minimum.Version\": \"14.0.4079.2\",");
            file.WriteLine("        \"SQL.Dac.Framework.x64.Minimum.Version\": \"14.0.4079.2\",");
            file.WriteLine("        \"SQL.ODBC.Minimum.Version\": \"13.1.4414.46\",");
            file.WriteLine("        \"SQLServer.Module.Minimum.Version\": \"21.1.18080\",");
            file.WriteLine("        \"VisualC++.x86.Minimum.Version\": \"14.0.24212.0\",");
            file.WriteLine("        \"VisualC++.x86.Debug.Minimum.Version\": \"14.0.0\",");
            file.WriteLine("        \"VisualC++.x64.Minimum.Version\": \"14.0.24212.0\",");
            file.WriteLine("        \"VisualC++.x64.Debug.Minimum.Version\": \"14.0.0\",");
            file.WriteLine("        \"WebPlatform.Minimum.Version\": \"5.0.50430.0\",");
            file.WriteLine("        \"WebDeploy.Minimum.Version\": \"10.0.1973\",");
            file.WriteLine("        \"WebPlatform.Download\": \"[JoinPath(variable('Temp.Location'),'WebPlatformInstaller_amd64_en-US.msi')]\",");
            file.WriteLine("        \"SQL.Clr.Types.x86.Download\": \"[JoinPath(variable('Temp.Location'),'SQLSysClrTypesx86.msi')]\",");
            file.WriteLine("        \"SQL.Clr.Types.x64.Download\": \"[JoinPath(variable('Temp.Location'),'SQLSysClrTypesx64.msi')]\",");
            file.WriteLine("        \"SQL.Dac.Framework.x86.Download\": \"[JoinPath(variable('Temp.Location'),'DacFrameworkx86.msi')]\",");
            file.WriteLine("        \"SQL.Dac.Framework.x64.Download\": \"[JoinPath(variable('Temp.Location'),'DacFrameworkx64.msi')]\",");
            file.WriteLine("        \"SQL.ODBC.Drivers.X64.Download\": \"[JoinPath(variable('Temp.Location'),'msodbcsql.msi')]\",");
            file.WriteLine("        \"DotNet.Framework.Download\": \"[JoinPath(variable('Temp.Location'),'DotNetFramework.exe')]\",");
            file.WriteLine("        \"DotNetHosting.Download\": \"[JoinPath(variable('Temp.Location'),'dotnet-hosting-win.exe')]\",");
            file.WriteLine("        \"VisualC++2015x86.Download\": \"[JoinPath(variable('Temp.Location'),'vc_redist.x86.exe')]\",");
            file.WriteLine("        \"VisualC++2015x64.Download\": \"[JoinPath(variable('Temp.Location'),'vc_redist.x64.exe')]\",");
            file.WriteLine("        \"WebPlatformCmd\": \"[JoinPath(Environment('ProgramFiles'),'Microsoft','Web Platform Installer','WebpiCmd-x64.exe')]\",");
            file.WriteLine("        \"Get.DotNet.Version\" : \"[GetItemPropertyValue(Path:parameter('DotNet4RegistryLocation'),Name:'Release')]\",");
            file.WriteLine("        \"Temp.Location\": \"[if(variable('Test.Temp.Location'),Environment('Temp'),parameter('TempLocation'))]\",");
            file.WriteLine("        \"Test.Temp.Location\": \"[equal(parameter('TempLocation'),'')]\",");
            file.WriteLine("        \"IISReset.Location\": \"[JoinPath(environment('windir'),'System32','IISReset.exe')]\",");
            file.WriteLine("        \"InstallArgs\": \"/passive /norestart\",");
            file.WriteLine("        \"ExitMessage\": \"[if(variable('Check.Reboot.Pending'),'Sitecore prerequisites are now installed, you must reboot your machine to allow prerequisite components installations to finish.','Sitecore prerequisites are now installed, YOU MUST launch a new PowerShell session to run further SIF configurations.')]\"");
            file.WriteLine("    },");
            file.WriteLine("    \"Register\": {");
            file.WriteLine("        \"Tasks\": {");
            file.WriteLine("            \"InstallModule\": \"Install-Module\",");
            file.WriteLine("            \"EnableWindowsOptionalFeature\": \"Enable-WindowsOptionalFeature\",");
            file.WriteLine("            \"StartProcess\" : \"Start-Process\",");
            file.WriteLine("            \"WriteInformation\": \"Write-Information\"");
            file.WriteLine("        },");
            file.WriteLine("        \"ConfigFunction\": {");
            file.WriteLine("            \"GetModule\": \"Get-Module\",");
            file.WriteLine("            \"GetPackage\": \"Get-Package\",");
            file.WriteLine("            \"GetItemPropertyValue\" : \"Get-ItemPropertyValue\",");
            file.WriteLine("            \"InvokeExpression\": \"Invoke-Expression\",");
            file.WriteLine("            \"SelectObject\": \"Select-Object\",");
            file.WriteLine("            \"TestPath\": \"Test-Path\"");
            file.WriteLine("        }");
            file.WriteLine("    },");
            file.WriteLine("    \"Tasks\": {");
            file.WriteLine("        \"DownloadVisualC++2015x86\": {");
            file.WriteLine("            \"Type\": \"DownloadFile\",");
            file.WriteLine("            \"Params\": {");
            file.WriteLine("                \"SourceUri\": \"[parameter('VisualC++2015x86Download')]\",");
            file.WriteLine("                \"DestinationPath\": \"[variable('VisualC++2015x86.Download')]\"");
            file.WriteLine("            },");
            file.WriteLine("            \"Skip\": \"[variable('VisualC++.x86.Compare')]\"");
            file.WriteLine("        },");
            file.WriteLine("        \"InstallC++2015x86\": {");
            file.WriteLine("            \"Type\": \"StartProcess\",");
            file.WriteLine("            \"Params\": {");
            file.WriteLine("                \"FilePath\": \"[variable('VisualC++2015x86.Download')]\",");
            file.WriteLine("                \"ArgumentList\": \"[variable('InstallArgs')]\",");
            file.WriteLine("                \"Wait\": true");
            file.WriteLine("            },");
            file.WriteLine("            \"Skip\": \"[variable('VisualC++.x86.Compare')]\"");
            file.WriteLine("        },");
            file.WriteLine("        \"DownloadVisualC++2015x64\": {");
            file.WriteLine("            \"Type\": \"DownloadFile\",");
            file.WriteLine("            \"Params\": {");
            file.WriteLine("                \"SourceUri\": \"[parameter('VisualC++2015x64Download')]\",");
            file.WriteLine("                \"DestinationPath\": \"[variable('VisualC++2015x64.Download')]\"");
            file.WriteLine("            },");
            file.WriteLine("            \"Skip\": \"[variable('VisualC++.x64.Compare')]\"");
            file.WriteLine("        },");
            file.WriteLine("        \"InstallC++2015x64\": {");
            file.WriteLine("            \"Type\": \"StartProcess\",");
            file.WriteLine("            \"Params\": {");
            file.WriteLine("                \"FilePath\": \"[variable('VisualC++2015x64.Download')]\",");
            file.WriteLine("                \"ArgumentList\": \"[variable('InstallArgs')]\",");
            file.WriteLine("                \"Wait\": true");
            file.WriteLine("            },");
            file.WriteLine("            \"Skip\": \"[variable('VisualC++.x64.Compare')]\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SQLServerModule\": {");
            file.WriteLine("            \"Type\": \"InstallModule\",");
            file.WriteLine("            \"Params\": {");
            file.WriteLine("                \"Name\": \"SQLServer\",");
            file.WriteLine("                \"Repository\" : \"[parameter('PSRepositoryPSGallery')]\",");
            file.WriteLine("                \"MinimumVersion\": \"[variable('SQLServer.Module.Minimum.Version')]\",");
            file.WriteLine("                \"AllowClobber\": true,");
            file.WriteLine("                \"Force\": true");
            file.WriteLine("            },");
            file.WriteLine("            \"Skip\": \"[variable('SQLServer.Module.Version.Compare')]\"");
            file.WriteLine("        },");
            file.WriteLine("        \"WindowsFeatures\": {");
            file.WriteLine("            \"Type\": \"EnableWindowsOptionalFeature\",");
            file.WriteLine("            \"Params\": {");
            file.WriteLine("                \"Online\": true,");
            file.WriteLine("                \"FeatureName\": [");
            file.WriteLine("                    \"IIS-WebServer\",");
            file.WriteLine("                    \"IIS-WebServerManagementTools\",");
            file.WriteLine("                    \"IIS-ASPNET45\",");
            file.WriteLine("                    \"NetFx4Extended-ASPNET45\",");
            file.WriteLine("                    \"WAS-ProcessModel\",");
            file.WriteLine("                    \"WAS-WindowsActivationService\"");
            file.WriteLine("                ],");
            file.WriteLine("                \"All\": true,");
            file.WriteLine("                \"NoRestart\": true");
            file.WriteLine("            }");
            file.WriteLine("        },");
            file.WriteLine("        \"DownloadWebPlatformInstaller\": {");
            file.WriteLine("            \"Type\": \"DownloadFile\",");
            file.WriteLine("            \"Params\": {");
            file.WriteLine("                \"SourceUri\": \"[parameter('WebPlatformDownload')]\",");
            file.WriteLine("                \"DestinationPath\": \"[variable('WebPlatform.Download')]\"");
            file.WriteLine("            },");
            file.WriteLine("            \"Skip\": \"[variable('WebPlatform.Version.Compare')]\"");
            file.WriteLine("        },");
            file.WriteLine("        \"InstallWebPlatformInstaller\": {");
            file.WriteLine("            \"Type\": \"StartProcess\",");
            file.WriteLine("            \"Params\": {");
            file.WriteLine("                \"FilePath\": \"[variable('WebPlatform.Download')]\",");
            file.WriteLine("                \"ArgumentList\": \"[variable('InstallArgs')]\",");
            file.WriteLine("                \"Wait\": true");
            file.WriteLine("            },");
            file.WriteLine("            \"Skip\": \"[variable('WebPlatform.Version.Compare')]\"");
            file.WriteLine("        },");
            file.WriteLine("        \"InstallWebDeploy3.6\": {");
            file.WriteLine("            \"Type\": \"StartProcess\",");
            file.WriteLine("            \"Params\": {");
            file.WriteLine("                \"FilePath\": \"[variable('WebPlatformCmd')]\",");
            file.WriteLine("                \"ArgumentList\": \"/Install /AcceptEULA /SuppressReboot /Products:WDeploy36PS\",");
            file.WriteLine("                \"Wait\": true");
            file.WriteLine("            },");
            file.WriteLine("            \"Skip\": \"[variable('WebDeploy.Version.Compare')]\"");
            file.WriteLine("        },");
            file.WriteLine("        \"InstallURLRewrite2\": {");
            file.WriteLine("            \"Type\": \"StartProcess\",");
            file.WriteLine("            \"Params\": {");
            file.WriteLine("                \"FilePath\": \"[variable('WebPlatformCmd')]\",");
            file.WriteLine("                \"ArgumentList\": \"/Install /AcceptEULA /SuppressReboot /Products:UrlRewrite2\",");
            file.WriteLine("                \"Wait\": true");
            file.WriteLine("            },");
            file.WriteLine("            \"Skip\": \"[variable('IIS.UrlRewrite.Version.Compare')]\"");
            file.WriteLine("        },");
            file.WriteLine("        \"DownloadSQLClrTypesx86\": {");
            file.WriteLine("            \"Type\": \"DownloadFile\",");
            file.WriteLine("            \"Params\": {");
            file.WriteLine("                \"SourceUri\": \"[parameter('SQLClrTypesx86Download')]\",");
            file.WriteLine("                \"DestinationPath\": \"[variable('SQL.Clr.Types.x86.Download')]\"");
            file.WriteLine("            },");
            file.WriteLine("            \"Skip\": \"[variable('SQL.CLR.Types.Version.Compare')]\"");
            file.WriteLine("        },");
            file.WriteLine("        \"InstallSQLClrTypesx86\": {");
            file.WriteLine("            \"Type\": \"StartProcess\",");
            file.WriteLine("            \"Params\": {");
            file.WriteLine("                \"FilePath\": \"[variable('SQL.Clr.Types.x86.Download')]\",");
            file.WriteLine("                \"ArgumentList\": \"[variable('InstallArgs')]\",");
            file.WriteLine("                \"Wait\": true");
            file.WriteLine("            },");
            file.WriteLine("            \"Skip\": \"[variable('SQL.CLR.Types.Version.Compare')]\"");
            file.WriteLine("        },");
            file.WriteLine("        \"DownloadSQLClrTypesx64\": {");
            file.WriteLine("            \"Type\": \"DownloadFile\",");
            file.WriteLine("            \"Params\": {");
            file.WriteLine("                \"SourceUri\": \"[parameter('SQLClrTypesx64Download')]\",");
            file.WriteLine("                \"DestinationPath\": \"[variable('SQL.Clr.Types.x64.Download')]\"");
            file.WriteLine("            },");
            file.WriteLine("            \"Skip\": \"[variable('SQL.CLR.Types.Version.Compare')]\"");
            file.WriteLine("        },");
            file.WriteLine("        \"InstallSQLClrTypesx64\": {");
            file.WriteLine("            \"Type\": \"StartProcess\",");
            file.WriteLine("            \"Params\": {");
            file.WriteLine("                \"FilePath\": \"[variable('SQL.Clr.Types.x64.Download')]\",");
            file.WriteLine("                \"ArgumentList\": \"[variable('InstallArgs')]\",");
            file.WriteLine("                \"Wait\": true");
            file.WriteLine("            },");
            file.WriteLine("            \"Skip\": \"[variable('SQL.CLR.Types.Version.Compare')]\"");
            file.WriteLine("        },");
            file.WriteLine("        \"DownloadSQLDacFrameworkx86\": {");
            file.WriteLine("            \"Type\": \"DownloadFile\",");
            file.WriteLine("            \"Params\": {");
            file.WriteLine("                \"SourceUri\": \"[parameter('SQLDacFrameworkx86Download')]\",");
            file.WriteLine("                \"DestinationPath\": \"[variable('SQL.Dac.Framework.x86.Download')]\"");
            file.WriteLine("            },");
            file.WriteLine("            \"Skip\": \"[variable('SQL.Dac.Framework.x86.Version.Compare')]\"");
            file.WriteLine("        },");
            file.WriteLine("        \"InstallSQLDacFrameworkx86\": {");
            file.WriteLine("            \"Type\": \"StartProcess\",");
            file.WriteLine("            \"Params\": {");
            file.WriteLine("                \"FilePath\": \"[variable('SQL.Dac.Framework.x86.Download')]\",");
            file.WriteLine("                \"ArgumentList\": \"[variable('InstallArgs')]\",");
            file.WriteLine("                \"Wait\": true");
            file.WriteLine("            },");
            file.WriteLine("            \"Skip\": \"[variable('SQL.Dac.Framework.x86.Version.Compare')]\"");
            file.WriteLine("        },");
            file.WriteLine("        \"DownloadSQLDacFrameworkx64\": {");
            file.WriteLine("            \"Type\": \"DownloadFile\",");
            file.WriteLine("            \"Params\": {");
            file.WriteLine("                \"SourceUri\": \"[parameter('SQLDacFrameworkx64Download')]\",");
            file.WriteLine("                \"DestinationPath\": \"[variable('SQL.Dac.Framework.x64.Download')]\"");
            file.WriteLine("            },");
            file.WriteLine("            \"Skip\": \"[variable('SQL.Dac.Framework.x64.Version.Compare')]\"");
            file.WriteLine("        },");
            file.WriteLine("        \"InstallSQLDacFrameworkx64\": {");
            file.WriteLine("            \"Type\": \"StartProcess\",");
            file.WriteLine("            \"Params\": {");
            file.WriteLine("                \"FilePath\": \"[variable('SQL.Dac.Framework.x64.Download')]\",");
            file.WriteLine("                \"ArgumentList\": \"[variable('InstallArgs')]\",");
            file.WriteLine("                \"Wait\": true");
            file.WriteLine("            },");
            file.WriteLine("            \"Skip\": \"[variable('SQL.Dac.Framework.x64.Version.Compare')]\"");
            file.WriteLine("        },");
            file.WriteLine("        \"DownloadSQLODBC\": {");
            file.WriteLine("            \"Type\": \"DownloadFile\",");
            file.WriteLine("            \"Params\": {");
            file.WriteLine("                \"SourceUri\": \"[parameter('SQLODBCDriversx64')]\",");
            file.WriteLine("                \"DestinationPath\": \"[variable('SQL.ODBC.Drivers.X64.Download')]\"");
            file.WriteLine("            },");
            file.WriteLine("            \"Skip\": \"[variable('SQL.ODBC.Version.Compare')]\"");
            file.WriteLine("        },");
            file.WriteLine("        \"InstallSQLODBC\": {");
            file.WriteLine("            \"Type\": \"StartProcess\",");
            file.WriteLine("            \"Params\": {");
            file.WriteLine("                \"FilePath\": \"[variable('SQL.ODBC.Drivers.X64.Download')]\",");
            file.WriteLine("                \"ArgumentList\": \"/passive /norestart IACCEPTMSODBCSQLLICENSETERMS=YES\",");
            file.WriteLine("                \"Wait\": true");
            file.WriteLine("            },");
            file.WriteLine("            \"Skip\": \"[variable('SQL.ODBC.Version.Compare')]\"");
            file.WriteLine("        },");
            file.WriteLine("        \"DownloadDotNetMultiTargeting\": {");
            file.WriteLine("            \"Type\": \"DownloadFile\",");
            file.WriteLine("            \"Params\": {");
            file.WriteLine("                \"SourceUri\": \"[parameter('DotNetHostingDownload')]\",");
            file.WriteLine("                \"DestinationPath\": \"[variable('DotNetHosting.Download')]\"");
            file.WriteLine("            },");
            file.WriteLine("            \"Skip\": \"[variable('Net.Hosting.Package')]\"");
            file.WriteLine("        },");
            file.WriteLine("        \"InstallDotNetMultiTargeting\": {");
            file.WriteLine("            \"Type\": \"StartProcess\",");
            file.WriteLine("            \"Params\": {");
            file.WriteLine("                \"FilePath\": \"[variable('DotNetHosting.Download')]\",");
            file.WriteLine("                \"ArgumentList\": \"[variable('InstallArgs')]\",");
            file.WriteLine("                \"Wait\": true");
            file.WriteLine("            },");
            file.WriteLine("            \"Skip\": \"[variable('Net.Hosting.Package')]\"");
            file.WriteLine("        },");
            file.WriteLine("        \"DownloadDotNetFramework\": {");
            file.WriteLine("            \"Type\" : \"DownloadFile\",");
            file.WriteLine("            \"Params\": {");
            file.WriteLine("                \"SourceUri\": \"[parameter('DotNetFrameworkDownload')]\",");
            file.WriteLine("                \"DestinationPath\": \"[variable('DotNet.Framework.Download')]\"");
            file.WriteLine("            },");
            file.WriteLine("            \"Skip\": \"[not(variable('Check.DotNetFramework.Version'))]\"");
            file.WriteLine("        },");
            file.WriteLine("        \"InstallDotNetFramework\": {");
            file.WriteLine("            \"Type\": \"StartProcess\",");
            file.WriteLine("            \"Params\": {");
            file.WriteLine("                \"FilePath\": \"[variable('DotNet.Framework.Download')]\",");
            file.WriteLine("                \"ArgumentList\": \"[variable('InstallArgs')]\",");
            file.WriteLine("                \"Wait\": true");
            file.WriteLine("            },");
            file.WriteLine("            \"Skip\": \"[not(variable('Check.DotNetFramework.Version'))]\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IISReset\": {");
            file.WriteLine("            \"Type\": \"StartProcess\",");
            file.WriteLine("            \"Params\": {");
            file.WriteLine("                \"FilePath\": \"[variable('IISReset.Location')]\",");
            file.WriteLine("                \"ArgumentList\": \"/Restart\",");
            file.WriteLine("                \"Wait\": true");
            file.WriteLine("            },");
            file.WriteLine("            \"Skip\": \"[variable('Net.Hosting.Package')]\"");
            file.WriteLine("        },");
            file.WriteLine("        \"InformUser\": {");
            file.WriteLine("            \"Type\": \"WriteInformation\",");
            file.WriteLine("            \"Params\": {");
            file.WriteLine("                \"MessageData\": \"[variable('ExitMessage')]\",");
            file.WriteLine("                \"InformationAction\": \"Continue\"");
            file.WriteLine("            }");
            file.WriteLine("        }");
            file.WriteLine("    }");
            file.WriteLine("}");

            file.Dispose();
        }

        void WriteWorkerFile(string path)
        {
            using var file = new StreamWriter(path);

            //file.WriteLine("[CmdletBinding(SupportsShouldProcess = $true)]");
            //file.WriteLine("[System.Diagnostics.CodeAnalysis.SuppressMessageAttribute(\"PSAvoidUsingPlainTextForPassword\", \"SitecorePassword\")]");
            //file.WriteLine("[System.Diagnostics.CodeAnalysis.SuppressMessageAttribute(\"PSAvoidUsingPlainTextForPassword\", \"RegistryPassword\")]");
            //file.WriteLine();
            file.WriteLine("param(");
            file.WriteLine("\t[Parameter(Mandatory = $false)]");
            file.WriteLine("\t[ValidateNotNullOrEmpty()]");
            file.WriteLine("\t[string]$InstallSourcePath = (Join-Path $PSScriptRoot \".\"),");
            file.WriteLine();
            file.WriteLine("\t[Parameter(Mandatory = $false)]");
            file.WriteLine("\t[ValidateNotNullOrEmpty()]");
            file.WriteLine("\t[string]$SitecoreUsername,");
            file.WriteLine();
            file.WriteLine("\t[Parameter(Mandatory = $false)]");
            file.WriteLine("\t[ValidateNotNullOrEmpty()]");
            file.WriteLine("\t[string]$SitecorePassword");
            file.WriteLine(")");
            file.WriteLine();
            file.WriteLine("if (-not(Test-Path \"" + ZipList.SitecoreDevSetupZip + ".zip\" -PathType Leaf)) {");
            file.WriteLine("$preference = $ProgressPreference");
            file.WriteLine("$ProgressPreference = \"SilentlyContinue\"");
            file.WriteLine("$sitecoreDownloadUrl = \"https://sitecoredev.azureedge.net\"");
            file.WriteLine("$packages = @{");
            file.WriteLine("\"" + zipVersions.ZipName + ".zip\" = \"" + zipVersions.Url + "\"");
            file.WriteLine("}");
            file.WriteLine();
            file.WriteLine("# download packages from Sitecore");
            file.WriteLine("$packages.GetEnumerator() | ForEach-Object {");
            file.WriteLine();
            file.WriteLine("\t$filePath = Join-Path $InstallSourcePath $_.Key");
            file.WriteLine("\t$fileUrl = $_.Value");
            file.WriteLine();
            file.WriteLine("\tif (Test-Path $filePath -PathType Leaf)");
            file.WriteLine("\t{");
            file.WriteLine("\t\tWrite-Host (\"Required package found: '{0}'\" -f $filePath)");
            file.WriteLine("\t}");
            file.WriteLine("\telse");
            file.WriteLine("\t{");
            file.WriteLine("\t\tif ($PSCmdlet.ShouldProcess($fileName))");
            file.WriteLine("\t\t{");
            file.WriteLine("\t\t\tWrite-Host (\"Downloading '{0}' to '{1}'...\" -f $fileUrl, $filePath)");
            file.WriteLine("\t\t\tInvoke-WebRequest -Uri $fileUrl -OutFile $filePath  -UseBasicParsing");
            file.WriteLine("\t\t}");
            file.WriteLine("\t\telse");
            file.WriteLine("\t\t{");
            file.WriteLine("\t\t\t# Download package");
            file.WriteLine("\t\t\tInvoke-WebRequest -Uri $fileUrl -OutFile $filePath -UseBasicParsing");
            file.WriteLine("\t\t}");
            file.WriteLine("\t}");
            file.WriteLine("}");
            file.WriteLine("}");
        }
        void WriteMainFile(string path)
        {
            using var file = new StreamWriter(path);

            //file.WriteLine("[CmdletBinding(SupportsShouldProcess = $true)]");
            //file.WriteLine("[System.Diagnostics.CodeAnalysis.SuppressMessageAttribute(\"PSAvoidUsingPlainTextForPassword\", \"SitecorePassword\")]");
            //file.WriteLine("[System.Diagnostics.CodeAnalysis.SuppressMessageAttribute(\"PSAvoidUsingPlainTextForPassword\", \"RegistryPassword\")]");
            //file.WriteLine();
            file.WriteLine("param(");
            file.WriteLine("[Parameter(Mandatory = $false)]");
            file.WriteLine("[ValidateNotNullOrEmpty()]");
            file.WriteLine("[string]$InstallSourcePath = (Join-Path $PSScriptRoot \".\"),");
            file.WriteLine();
            file.WriteLine("[Parameter(Mandatory = $false)]");
            file.WriteLine("[ValidateNotNullOrEmpty()]");
            file.WriteLine("[string]$SitecoreUsername,");
            file.WriteLine();
            file.WriteLine("[Parameter(Mandatory = $false)]");
            file.WriteLine("[ValidateNotNullOrEmpty()]");
            file.WriteLine("[string]$SitecorePassword");
            file.WriteLine(")");
            file.WriteLine();

            file.WriteLine("$preference = $ProgressPreference");
            file.WriteLine("$ProgressPreference = \"SilentlyContinue\"");
            file.WriteLine("if (-not(Test-Path '" + zipVersions.ZipName + ".zip' -PathType Leaf))");
            file.WriteLine("{");
            file.WriteLine(".\\" + SCIASettings.FilePrefixAppString + "DownloadandSetupAllPrereqs.ps1 -InstallSourcePath $InstallSourcePath");
            file.WriteLine("}");
            file.WriteLine("Expand-Archive -Force -LiteralPath '" + zipVersions.ZipName + ".zip' -DestinationPath \".\\" + zipVersions.ZipName + "\"");
            file.WriteLine("if ((Test-Path '" + zipVersions.ZipName + ".zip' -PathType Leaf))");
            file.WriteLine("{");
            file.WriteLine("Copy-Item -Force -Path \"license.xml\" -Destination \".\\" + zipVersions.ZipName + "\\license.xml\"");
            file.WriteLine("}");
            file.WriteLine();
            file.WriteLine(
                "$ProgressPreference = $preference");
            file.WriteLine(
                "Write-Host \"DONE\"");

            file.Dispose();
        }

        void WriteXP0MainFile(string path)
        {
            using var file = new StreamWriter(path);

            //file.WriteLine("[CmdletBinding(SupportsShouldProcess = $true)]");
            //file.WriteLine("[System.Diagnostics.CodeAnalysis.SuppressMessageAttribute(\"PSAvoidUsingPlainTextForPassword\", \"SitecorePassword\")]");
            //file.WriteLine("[System.Diagnostics.CodeAnalysis.SuppressMessageAttribute(\"PSAvoidUsingPlainTextForPassword\", \"RegistryPassword\")]");
            //file.WriteLine();
            file.WriteLine("param(");
            file.WriteLine("[Parameter(Mandatory = $false)]");
            file.WriteLine("[ValidateNotNullOrEmpty()]");
            file.WriteLine("[string]$InstallSourcePath = (Join-Path $PSScriptRoot \".\"),");
            file.WriteLine();
            file.WriteLine("[Parameter(Mandatory = $false)]");
            file.WriteLine("[ValidateNotNullOrEmpty()]");
            file.WriteLine("[string]$SitecoreUsername,");
            file.WriteLine();
            file.WriteLine("[Parameter(Mandatory = $false)]");
            file.WriteLine("[ValidateNotNullOrEmpty()]");
            file.WriteLine("[string]$SitecorePassword");
            file.WriteLine(")");
            file.WriteLine();

            file.WriteLine("$preference = $ProgressPreference");
            file.WriteLine("$ProgressPreference = \"SilentlyContinue\"");
            file.WriteLine("if (-not(Test-Path '" + zipVersions.ZipName + ".zip' -PathType Leaf))");
            file.WriteLine("{");
            file.WriteLine(".\\" + SCIASettings.FilePrefixAppString + "DownloadandSetupAllXP0SIFPrereqs.ps1 -InstallSourcePath $InstallSourcePath");
            file.WriteLine("}");
            file.WriteLine("Expand-Archive -Force -LiteralPath '" + zipVersions.ZipName + ".zip' -DestinationPath \".\\" + zipVersions.ZipName + "\"");
            file.WriteLine("if ((Test-Path '" + zipVersions.ZipName + "' -PathType Container))");
            file.WriteLine("\t{");
            file.WriteLine("\tExpand-Archive -Force -LiteralPath '.\\" + destFolder + "\\" + prereqs.Where(p => p.PrerequisiteKey == "XP0").ToList().FirstOrDefault().PrerequisiteName + ".zip' -DestinationPath \".\\" + zipVersions.ZipName + "\"");
            file.WriteLine("}");
            file.WriteLine("if ((Test-Path '" + zipVersions.ZipName + "' -PathType Container))");
            file.WriteLine("{");
            file.WriteLine("Copy-Item -Force -Path \"license.xml\" -Destination \".\\" + zipVersions.ZipName + "\\"  + "\\license.xml\"");
            file.WriteLine("}");
            file.WriteLine();
            file.WriteLine(
                "$ProgressPreference = $preference");
            file.WriteLine(
                "Write-Host \"DONE\"");

            file.Dispose();
        }



        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (CommonFunctions.FileSystemEntryExists(destFolder, null,"folder")) return;
            if (!CommonFunctions.FileSystemEntryExists("license.xml", null, "file")) {
                SetStatusMessage("License file missing in the exe location...", Color.Red);
                return;
            }

            switch (Version.SitecoreVersion)
            {
                case "10.2.0":
                case "10.1.1":
                case "10.1.0":
                case "10.0.1":
                case "10.0":
                case "9.3":
                case "9.2":
                    WriteWorkerFile(@".\" + SCIASettings.FilePrefixAppString + "DownloadandSetupAllPrereqs.ps1");
                    WriteMainFile(@".\" + SCIASettings.FilePrefixAppString + "DownloadandExpandSifZip.ps1");

                    CommonFunctions.LaunchPSScript(@".\" + SCIASettings.FilePrefixAppString + "DownloadandExpandSifZip -InstallSourcePath \".\"");
                    break;
                case "9.1":
                case "9.1.1":
                case "9.0":
                case "9.0.1":
                case "9.0.2":
                    WriteWorkerFile(@".\" + SCIASettings.FilePrefixAppString + "DownloadandSetupAllXP0SIFPrereqs.ps1");
                    WriteXP0MainFile(@".\" + SCIASettings.FilePrefixAppString + "DownloadandExpandXP0SifZip.ps1");

                    CommonFunctions.LaunchPSScript(@".\" + SCIASettings.FilePrefixAppString + "DownloadandExpandXP0SifZip -InstallSourcePath \".\"");
                    break;
                default:
                    break;
            }



        }

        private void btnPrerequisites_Click(object sender, EventArgs e)
        {
            switch (Version.SitecoreVersion)
            {
                case "10.2.0":
                case "10.1.1":
                case "10.1.0":
                case "10.0.1":
                case "10.0":
                case "9.3":
                case "9.2":
                case "9.1.1":
                    if (CommonFunctions.FileSystemEntryExists(destFolder, null, "folder"))
                    {
                        CommonFunctions.LaunchPSScript(@"Install-SitecoreConfiguration -Path .\Prerequisites.json", destFolder);
                    }
                    break;
                case "9.0":
                case "9.0.1":
                case "9.0.2":
                    WritePrerequisitesFile(@".\" + destFolder + "\\Prerequisites.json");//need to generate this for new machines since 9.0.2 doesn't provide a prerequisites.json in the zip
                    CommonFunctions.LaunchPSScript(@"Install-SitecoreConfiguration -Path .\Prerequisites.json", destFolder);
                    break;
                default:
                    break;
            }
            SetStatusMessage("Restart machine for prerequisites install to take effect...", Color.Red);
        }

    }
}
