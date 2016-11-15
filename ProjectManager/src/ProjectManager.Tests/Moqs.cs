using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectManager.Core;
using Moq;


namespace ProjectManager.Tests
{
    public static class Moqs 
    {
        public static INetworkUtilities NetworkUtilitiesMoq()
        {
            Mock<INetworkUtilities> mock = new Mock<INetworkUtilities>();
            mock.Setup(x => x.IsNetworkAvailable()).Returns(true);
            mock.Setup(x => x.VerifyDBServerConnectivity(It.IsNotNull<string>())).Returns(true);
            mock.Setup(x => x.VerifyDBServerConnectivity(It.Is<string>(z => z == null))).Returns(false);
            mock.Setup(x => x.VerifyHttpServerAvailability(It.IsNotNull<string>())).Returns(true);
            mock.Setup(x => x.VerifyHttpServerAvailability(It.Is<string>(z => z == null))).Returns(false);
            return mock.Object;
        }
    }
}
