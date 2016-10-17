using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;

namespace ProjectManager.Tests
{
    [TestFixture]
    public class Class1
    {
        
        public Class1()
        {
            
        }

        [Test]
        public void Test1()
        {
            string x = Microsoft.Extensions.PlatformAbstractions.PlatformServices.Default.Application.ApplicationBasePath;

        }
    }
}
