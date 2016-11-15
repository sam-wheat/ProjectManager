using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectManager.Core;

namespace ProjectManager.Gateway
{
    public class HttpEndPointValidator : IEndPointValidator
    {
        private INetworkUtilities networkUtilities;

        public HttpEndPointValidator(INetworkUtilities networkUtilities)
        {
            this.networkUtilities = networkUtilities;
        }

        public bool IsInterfaceAlive(IEndPointConfiguration endPoint)
        {
            return networkUtilities.VerifyHttpServerAvailability(endPoint.ConnectionString);
        }
    }
}
