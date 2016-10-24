using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;
using ProjectManager.Domain;
using ProjectManager.Model;

namespace ProjectManager.Tests
{
    [TestFixture]
    public class UsersServiceTests : BaseTest
    {
        
        public UsersServiceTests()
        {
            
        }

        [Test]
        public void Test1()
        {
            string errorMsg = null;
            ServiceClient.OfType<IUsersService>().Try(x => x.SaveUser(new Model.Domain.User { Name = "User 1", Password = "x", IsActive = true }, out errorMsg));

        }
    }
}
