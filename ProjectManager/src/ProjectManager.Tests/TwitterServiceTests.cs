using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;
using ProjectManager.Domain.Twitter;
using ProjectManager.Model.Twitter;

namespace ProjectManager.Tests
{
    [TestFixture]
    public class TwitterServiceTests : BaseTest
    {
        [Test]
        public void Test1()
        {
            ServiceClient.OfType<ITwitter>().Try(x => x.SendTweet(new Tweet { Message = "Hello World" }));
        }
    }
}
