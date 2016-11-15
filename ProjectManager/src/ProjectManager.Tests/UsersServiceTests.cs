﻿using System;
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

namespace ProjectManager.Tests
{

    [TestFixture]
    public class UsersServiceTests : BaseTest
    {
        IServiceGateway<IUsersService> usersService;

        public UsersServiceTests()
        {
            usersService = this.container.Resolve<IServiceGateway<IUsersService>>();
        }

        [Test]
        public void Test1()
        {
            IAsyncServiceResult result = usersService.TryAsync(x => x.SaveUser(new User { Name = "User 1", Password = "x", IsActive = true })).Result;
            IAsyncServiceResult<User> userResult = usersService.TryAsync(x => x.GetUser("User 1", "x")).Result;
            Assert.IsNotNull(userResult.Data);
            Assert.AreEqual("User 1", userResult.Data.Name);
            Test2();
        }
        [Test]
        public void Test2()
        {
            string x = ConfigManager.AppCurrentDir;

            //string url =  "http://localhost:51513/api/users/GetUser?UserName=User 1&Password=x";
            //string json = new HttpClient().GetAsync(url).Result.Content.ReadAsStringAsync().Result;

        }
    }
}
