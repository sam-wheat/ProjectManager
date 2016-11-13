using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectManager.Core;

namespace ProjectManager.Gateway
{
    public class HttpEndPointValidator : IEndPointValidator
    {
        public bool IsInterfaceAlive(IEndPointConfiguration endPoint)
        {
            bool result = false;
            return result;
        }
    }
}
