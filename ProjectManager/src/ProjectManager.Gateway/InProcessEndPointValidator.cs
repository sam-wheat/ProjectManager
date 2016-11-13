using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectManager.Core;

namespace ProjectManager.Gateway
{
    public class InProcessEndPointValidator : IEndPointValidator
    {
        private INetworkUtilities networkUtilities;

        public InProcessEndPointValidator(INetworkUtilities networkUtilities)
        {
            this.networkUtilities = networkUtilities;
        }

        public virtual bool IsInterfaceAlive(IEndPointConfiguration endPoint)
        {
            return networkUtilities.VerifyDBServerConnectivity(endPoint.ConnectionString);
        }
    }
}
