using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using NUnit;
using Moq;
using NUnit.Framework;
using ProjectManager.Core;
using ProjectManager.Gateway;
using ProjectManager.Domain;
using ProjectManager.Model.Domain;
using ProjectManager.Services;
using ProjectManager.Services.REST;

namespace ProjectManager.Tests
{
    [TestFixture]
    public class ServiceClientTests : BaseTest
    {
        [Test]
        public void InProcess_ReturnsUser()
        {
            Mock<INetworkUtilities> mock = new Mock<INetworkUtilities>();
            mock.Setup(x => x.VerifyDBServerConnectivity(It.IsAny<string>())).Returns(true);
            INetworkUtilities networkUtilitiesMoq = mock.Object;

            ContainerBuilder builder = new ContainerBuilder();
            AutofacRegistrationHelper registrationHelper = new AutofacRegistrationHelper(builder);
            registrationHelper.RegisterEndPoints(ConfigManager.EndPoints);
            new Services.ServiceRegistry(registrationHelper).Register();
            new Services.REST.ServiceRegistry(registrationHelper).Register();
            builder.RegisterModule(new ProjectManager.Core.IOCModule());
            builder.RegisterModule(new ProjectManager.Gateway.IOCModule());
            builder.RegisterModule(new ProjectManager.Services.IOCModule());
            builder.RegisterModule(new ProjectManager.Services.REST.IOCModule());
            builder.RegisterModule(new Tests.IOCModule());
            builder.RegisterInstance(networkUtilitiesMoq).As<INetworkUtilities>();
            IContainer container = builder.Build();

            IServiceClient<IUsersService> usersService = container.Resolve<IServiceClient<IUsersService>>();
            IAsyncServiceResult result = usersService.TryAsync(x => x.SaveUser(new User { Name = "User 1", Password = "x", IsActive = true })).Result;
            IAsyncServiceResult<User> userResult = usersService.TryAsync(x => x.GetUser("User 1", "x")).Result;
            Assert.IsNotNull(userResult.Data);
            Assert.AreEqual("User 1", userResult.Data.Name);
        }

        [Test]
        public void REST_ReturnsUser()
        {
            Mock<INetworkUtilities> mock = new Mock<INetworkUtilities>();
            mock.Setup(x => x.VerifyDBServerConnectivity(It.IsAny<string>())).Returns(false);
            mock.Setup(x => x.VerifyHttpServerAvailability(It.IsAny<string>())).Returns(true);
            INetworkUtilities networkUtilitiesMoq = mock.Object;

            ContainerBuilder builder = new ContainerBuilder();
            AutofacRegistrationHelper registrationHelper = new AutofacRegistrationHelper(builder);
            registrationHelper.RegisterEndPoints(ConfigManager.EndPoints);
            new Services.ServiceRegistry(registrationHelper).Register();
            new Services.REST.ServiceRegistry(registrationHelper).Register();
            builder.RegisterModule(new ProjectManager.Core.IOCModule());
            builder.RegisterModule(new ProjectManager.Gateway.IOCModule());
            builder.RegisterModule(new ProjectManager.Services.IOCModule());
            builder.RegisterModule(new ProjectManager.Services.REST.IOCModule());
            builder.RegisterModule(new Tests.IOCModule());
            builder.RegisterInstance(networkUtilitiesMoq).As<INetworkUtilities>();
            IContainer container = builder.Build();

            IServiceClient<IUsersService> usersService = container.Resolve<IServiceClient<IUsersService>>();
            IAsyncServiceResult result = usersService.TryAsync(x => x.SaveUser(new User { Name = "User 1", Password = "x", IsActive = true })).Result;
            IAsyncServiceResult<User> userResult = usersService.TryAsync(x => x.GetUser("User 1", "x")).Result;
            Assert.IsNotNull(userResult.Data);
            Assert.AreEqual("User 1", userResult.Data.Name);
        }


        [Test]
        public void WCF_ReturnsUser()
        {
            Mock<INetworkUtilities> mock = new Mock<INetworkUtilities>();
            mock.Setup(x => x.VerifyDBServerConnectivity(It.IsAny<string>())).Returns(false);
            mock.Setup(x => x.VerifyHttpServerAvailability(It.Is<string>(c => c == ""))).Returns(true);
            INetworkUtilities networkUtilitiesMoq = mock.Object;

            ContainerBuilder builder = new ContainerBuilder();
            AutofacRegistrationHelper registrationHelper = new AutofacRegistrationHelper(builder);
            registrationHelper.RegisterEndPoints(ConfigManager.EndPoints);
            new Services.ServiceRegistry(registrationHelper).Register();
            new Services.REST.ServiceRegistry(registrationHelper).Register();
            builder.RegisterModule(new ProjectManager.Core.IOCModule());
            builder.RegisterModule(new ProjectManager.Gateway.IOCModule());
            builder.RegisterModule(new ProjectManager.Services.IOCModule());
            builder.RegisterModule(new ProjectManager.Services.REST.IOCModule());
            builder.RegisterModule(new Tests.IOCModule());
            builder.RegisterInstance(networkUtilitiesMoq).As<INetworkUtilities>();
            IContainer container = builder.Build();

            IServiceClient<IUsersService> usersService = container.Resolve<IServiceClient<IUsersService>>();
            IAsyncServiceResult result = usersService.TryAsync(x => x.SaveUser(new User { Name = "User 1", Password = "x", IsActive = true })).Result;
            IAsyncServiceResult<User> userResult = usersService.TryAsync(x => x.GetUser("User 1", "x")).Result;
            Assert.IsNotNull(userResult.Data);
            Assert.AreEqual("User 1", userResult.Data.Name);
        }
    }
}
