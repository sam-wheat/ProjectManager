using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectManager.Core;

namespace ProjectManager.Gateway
{
    public class EndPointTypeResolver : IEndPointTypeResolver
    {
        private Dictionary<Type, IAPI> EndPointDict;

        public EndPointTypeResolver()
        {
            EndPointDict = new Dictionary<Type, IAPI>();
        }


        public IAPI Resolve(Type serviceType)
        {
            IAPI api;
            EndPointDict.TryGetValue(serviceType, out api);
            return api;
        }

        public void Register(Type serviceType, IAPI api)
        {
            EndPointDict[serviceType] = api;
        }
    }
}
