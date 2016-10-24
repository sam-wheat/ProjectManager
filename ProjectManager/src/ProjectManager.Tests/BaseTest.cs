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

namespace ProjectManager.Tests
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class BaseTest
    {
        protected IContainer container { get; private set; }
        protected IServiceClient ServiceClient { get; private set; }

        public BaseTest()
        {
            BuildContainer();
            CreateServiceClient();
            InitializeDatabase();
            SeedDatabase();
        }

        protected void BuildContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterModule(new ProjectManager.Core.IOCModule());
            builder.RegisterModule(new ProjectManager.Gateway.IOCModule());
            builder.RegisterModule(new ProjectManager.Services.IOCModule());
            
            container = builder.Build();
        }

        protected void CreateServiceClient()
        {
            INamedConnectionString conn = container.Resolve<INamedConnectionString>();
            conn.ConnectionString = ProjectManager.Core.DataConfigManager.ConnectionString;
            ServiceClient = container.Resolve<IServiceClient>();
        }

        protected void InitializeDatabase()
        {
            DropAndRecreateInitializer initializer = container.Resolve<DropAndRecreateInitializer>();
            initializer.DropAndRecreateDb();
        }

        protected void SeedDatabase()
        {
            //ServiceClient.OfType<IUsersService>().Try(x => x.SeedDB()).Wait();
        }
    }
}
