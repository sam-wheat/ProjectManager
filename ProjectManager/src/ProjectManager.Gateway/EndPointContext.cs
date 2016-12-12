using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectManager.Core;

namespace ProjectManager.Gateway
{
    public class EndPointContext
    {
        public IEndPointConfiguration CurrentEndPoint { get; set; }
    }
}
