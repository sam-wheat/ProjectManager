using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectManager.Core;

namespace ProjectManager.Gateway
{
    public class EndPointConfigFactory<T>
    {
        IEnumerable<Lazy<T, string>> EndPointConfigs;

        public EndPointConfigFactory(IEnumerable<Lazy<T,string>> endPointConfigs)
        {
            EndPointConfigs = endPointConfigs;
        }
    }
}
