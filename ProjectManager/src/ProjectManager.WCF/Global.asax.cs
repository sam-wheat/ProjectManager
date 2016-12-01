using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Autofac;
using Autofac.Integration.Wcf;
using ProjectManager.Core;
using ProjectManager.Gateway;
using ProjectManager.Domain;
using ProjectManager.Model.Domain;
using ProjectManager.Services;
using ProjectManager.Services.REST;

// http://docs.autofac.org/en/latest/integration/wcf.html

namespace ProjectManager.WCF
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            ContainerBuilder builder = new ContainerBuilder();
            AutofacRegistrationHelper registrationHelper = new AutofacRegistrationHelper(builder);
            registrationHelper.RegisterEndPoints(CreateEndPoints());
            new ProjectManager.Services.ServiceRegistry(registrationHelper).Register();
            new Services.REST.ServiceRegistry(registrationHelper).Register();

            //new ServiceRegistry(registrationHelper).Register();
            builder.RegisterType<UsersService>();


            builder.RegisterModule(new ProjectManager.Core.IOCModule());
            builder.RegisterModule(new ProjectManager.Gateway.IOCModule());
            builder.RegisterModule(new ProjectManager.Services.IOCModule());
            builder.RegisterModule(new ProjectManager.Services.REST.IOCModule());
            IContainer container = builder.Build();
            AutofacHostFactory.Container = container;
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        private IEnumerable<IEndPointConfiguration> CreateEndPoints()
        {
            string apiName = APIName.ProjectManager.ToString();

            IEnumerable<IEndPointConfiguration> endPoints = new List<IEndPointConfiguration>
            {
                new EndPointConfiguration {Name="Horrible_SQL", API_Name=apiName, EndPointType = EndPointType.InProcess, Preference = 1, IsActive=true, ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=ProjectManager;Integrated Security=True;MultipleActiveResultSets=True"},
                new EndPointConfiguration {Name="Horrible_REST", API_Name=apiName, EndPointType = EndPointType.REST, Preference = 2, IsActive=true, ConnectionString = "http://localhost:51513/"},
            };
            return endPoints;
        }
    }
}