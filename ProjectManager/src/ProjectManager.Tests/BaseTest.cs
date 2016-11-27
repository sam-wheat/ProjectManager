using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Configuration;
using Autofac;
using ProjectManager.Core;
using ProjectManager.Domain;
using ProjectManager.Services;
using ProjectManager.Services.Integration;
using ProjectManager.Services.REST;
using ProjectManager.Gateway;

namespace ProjectManager.Tests
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class BaseTest
    {
        protected IContainer container { get; private set; }
        protected IConfiguration Configuration { get; set; }

        public BaseTest()
        {
            // https://weblog.west-wind.com/posts/2016/may/23/strongly-typed-configuration-settings-in-aspnet-core
            Configuration = ConfigManager.GetConfigurationRoot();
            BuildContainer();
            InitializeDatabase();
        }

        protected void BuildContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();
            AutofacRegistrationHelper registrationHelper = new AutofacRegistrationHelper(builder);
            registrationHelper.RegisterEndPoints(ConfigManager.EndPoints);
            registrationHelper.RegisterService<DatabaseUtilitiesServicecs, IDatabaseUtilitiesService>(EndPointType.InProcess, APIName.ProjectManager.ToString());
            builder.RegisterModule(new ProjectManager.Core.IOCModule());
            builder.RegisterModule(new ProjectManager.Gateway.IOCModule());
            builder.RegisterModule(new ProjectManager.Services.IOCModule());
            builder.RegisterModule(new ProjectManager.Services.REST.IOCModule());
            builder.RegisterModule(new Tests.IOCModule());
            container = builder.Build();
        }

        protected void InitializeDatabase()
        {
            IServiceClient<IDatabaseUtilitiesService> utilities = container.Resolve<IServiceClient<IDatabaseUtilitiesService>>();
            utilities.Try(x => x.RecreateDb());
        }
    }
}
