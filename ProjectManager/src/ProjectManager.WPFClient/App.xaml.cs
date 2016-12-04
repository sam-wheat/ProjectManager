using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Autofac;
using ProjectManager.Core;
using ProjectManager.Gateway;
using ProjectManager.Domain;

namespace ProjectManager.WPFClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IContainer Container;

        public App()
        {
            
        }

        public void CreateContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();
            AutofacRegistrationHelper registrationHelper = new AutofacRegistrationHelper(builder);
            registrationHelper.RegisterEndPoints(CreateEndPoints());
            new Services.ServiceRegistry(registrationHelper).Register();
            new Services.REST.ServiceRegistry(registrationHelper).Register();
            new Services.WCF.ServiceRegistry(registrationHelper).Register();
            builder.RegisterModule(new ProjectManager.Core.IOCModule());
            builder.RegisterModule(new ProjectManager.Gateway.IOCModule());
            builder.RegisterModule(new ProjectManager.Services.IOCModule());
            builder.RegisterModule(new ProjectManager.Services.REST.IOCModule());
            builder.RegisterModule(new IOCModule());
            Container = builder.Build();
        }


        private IEnumerable<IEndPointConfiguration> CreateEndPoints()
        {
            string apiName = APIName.ProjectManager.ToString();

            IEnumerable<IEndPointConfiguration> endPoints = new List<IEndPointConfiguration>
            {
                new EndPointConfiguration {Name="Horrible_SQL", API_Name=apiName, EndPointType = EndPointType.InProcess, Preference = 3, IsActive=true, ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=ProjectManager;Integrated Security=True;MultipleActiveResultSets=True"},
                new EndPointConfiguration {Name="Horrible_REST", API_Name=apiName, EndPointType = EndPointType.REST, Preference = 2, IsActive=true, ConnectionString = "http://localhost:51513/"},
                new EndPointConfiguration {Name="Horrible_WCF", API_Name=apiName, EndPointType = EndPointType.WCF, Preference = 1, IsActive=true, ConnectionString = "http://localhost:62136/"},
            };
            return endPoints;
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

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            CreateContainer();
            MainWindow window = Container.Resolve<MainWindow>();
            window.ShowDialog();
        }
    }
}
