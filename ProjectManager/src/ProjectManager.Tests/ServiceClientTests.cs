using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using NUnit;
using NUnit.Framework;
using ProjectManager.Core;
using ProjectManager.Gateway;
using ProjectManager.Domain;
using ProjectManager.Model;

namespace ProjectManager.Tests
{
    [TestFixture]
    public class ServiceClientTests : BaseTest
    {
        public void InProcess_ReturnsUser()
        {
            ILifetimeScope scope = container.Resolve<ILifetimeScope>();
            INetworkUtilities networkUtilities = Moqs.NetworkUtilitiesMoq();
        }
    }
}
