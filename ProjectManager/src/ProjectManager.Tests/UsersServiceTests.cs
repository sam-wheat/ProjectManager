using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Autofac;
using NUnit;
using NUnit.Framework;
using ProjectManager.Core;
using ProjectManager.Domain;
using ProjectManager.Model.Domain;
using ProjectManager.Gateway;

namespace ProjectManager.Tests
{

    [TestFixture]
    public class UsersServiceTests : BaseTest
    {
        IServiceClient<IUsersService> usersService;

        public UsersServiceTests()
        {
            //usersService = this.container.Resolve<IServiceClient<IUsersService>>();
        }

        [Test]
        public void Test1()
        {
            IAsyncServiceResult result = usersService.TryAsync(x => x.SaveUser(new User { Name = "User 1", Password = "x", IsActive = true })).Result;
            IAsyncServiceResult<User> userResult = usersService.TryAsync(x => x.GetUser("User 1", "x")).Result;
            Assert.IsNotNull(userResult.Data);
            Assert.AreEqual("User 1", userResult.Data.Name);
        }


        [Test]
        public void Test2()
        {
            ContainerBuilder builder = new ContainerBuilder();
            AutoFacRegistrationHelper registrationHelper = new AutoFacRegistrationHelper(builder);
            registrationHelper.RegisterEndPoints(ConfigManager.EndPoints);
            registrationHelper.RegisterAPI(typeof(IUsersService), APIName.ProjectManager.ToString());
            builder.RegisterModule(new ProjectManager.Core.IOCModule());
            builder.RegisterModule(new ProjectManager.Gateway.IOCModule());
            builder.RegisterModule(new ProjectManager.Services.IOCModule());
            builder.RegisterModule(new ProjectManager.Services.REST.IOCModule());
            builder.RegisterModule(new Tests.IOCModule());
            IContainer container = builder.Build();
            IServiceClient<IUsersService> usersService = container.Resolve<IServiceClient<IUsersService>>();
            IAsyncServiceResult result = usersService.TryAsync(x => x.SaveUser(new User { Name = "User 1", Password = "x", IsActive = true })).Result;
            IAsyncServiceResult<User> userResult = usersService.TryAsync(x => x.GetUser("User 1", "x")).Result;
            Assert.IsNotNull(userResult.Data);
            Assert.AreEqual("User 1", userResult.Data.Name);
        }
    }
}
