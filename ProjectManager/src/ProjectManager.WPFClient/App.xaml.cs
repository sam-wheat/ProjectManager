using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectManager.WPFClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public App()
        {

        }

        public static bool VerifyLocalDBInstallation(out string errorMsg)
        {
            errorMsg = "";

            if (Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Microsoft SQL Server Local DB", false) == null)
            {
                errorMsg = "Microsoft SQL Server Local DB must be installed prior to running this application." + Environment.NewLine +
                    "Note that Local DB is not the same as SQL Server Express." + Environment.NewLine +
                    "Local DB can be downloaded from this link: http://www.microsoft.com/en-us/download/details.aspx?id=29062";
                return false;
            }
            return true;
        }
    }
}
