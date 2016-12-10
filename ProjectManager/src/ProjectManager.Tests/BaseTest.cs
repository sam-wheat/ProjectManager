using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using Autofac;
using ProjectManager.Core;
using ProjectManager.Domain;
using ProjectManager.Services;
using ProjectManager.Services.Integration;
using ProjectManager.Services.REST;
using ProjectManager.Gateway;

namespace ProjectManager.Tests
{
    public class BaseTest
    {
        protected IContainer container { get; private set; }
        protected IConfiguration Configuration { get; set; }
        protected TestServer API_Server;
        protected TestServer WCF_Server;
        
        public BaseTest()
        {
            Configuration = ConfigManager.GetConfigurationRoot();
            BuildContainer();
            InitializeDatabase();
           // API_Server = new TestServer(new WebHostBuilder().UseStartup<ProjectManager.API.Startup>());
        }

        protected void BuildContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();
            AutofacRegistrationHelper registrationHelper = new AutofacRegistrationHelper(builder);
            registrationHelper.RegisterEndPoints(ConfigManager.EndPoints);
            builder.RegisterModule(new ProjectManager.Core.IOCModule());
            builder.RegisterModule(new ProjectManager.Gateway.IOCModule());
            builder.RegisterModule(new ProjectManager.Services.IOCModule());
            builder.RegisterModule(new ProjectManager.Services.REST.IOCModule());
            container = builder.Build();
        }

        protected void InitializeDatabase()
        {
            ServiceDbContextOptions options = new ServiceDbContextOptions(() => ConfigManager.EndPoints.Single(y => y.Name == "Horrible_SQL"));
            Db db = new Db(options);
            DatabaseUtilitiesService svc = new DatabaseUtilitiesService(db);
            svc.RecreateDb();
        }
    }
}
