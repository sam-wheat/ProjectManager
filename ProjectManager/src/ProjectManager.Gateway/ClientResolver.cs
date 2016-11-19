using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectManager.Core;
using Autofac;

namespace ProjectManager.Gateway
{
    public class ClientResolver : IClientResolver
    {
        public static IEndPointConfiguration CurrentEndPoint;
        private INetworkUtilities networkUtilities;
        private Dictionary<Type, IAPI> APIDict;

        public ClientResolver(INetworkUtilities networkUtilities)
        {
            this.networkUtilities = networkUtilities;
            APIDict = new Dictionary<Type, IAPI>();
        }

        public void RegisterService(Type serviceType, IAPI api)
        {
            APIDict[serviceType] = api;
        }

        public virtual T ResolveClient<T>(ILifetimeScope container)
        {
            T client = default(T);

            IAPI api;
            APIDict.TryGetValue(typeof(T), out api);


            if (api == null)
                return client;  // Service is not registered to any API

            foreach (string endPointName in api.EndPointNames)
            {
                IEndPointConfiguration endPoint = container.ResolveKeyed<IEndPointConfiguration>(endPointName);
                client = container.ResolveKeyed<T>(endPoint.EndPointType);

                if (client == null)
                    continue;   

                IEndPointValidator validator = container.ResolveKeyed<IEndPointValidator>(endPoint.EndPointType);

                if (!validator.IsInterfaceAlive(endPoint))
                    continue;

                ClientResolver.CurrentEndPoint = endPoint;
                break;
            }
            return client;
        }
    }
}
