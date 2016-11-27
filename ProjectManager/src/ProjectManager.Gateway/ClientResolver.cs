using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectManager.Core;

namespace ProjectManager.Gateway
{
    public class ClientResolver<T> : IClientResolver<T>
    {
        private Func<Type, IAPI> apiFactory;
        private Func<EndPointType, T> serviceFactory;
        private Func<EndPointType, IEndPointValidator> validatorFactory;
        private EndPointInstance endPointInstance;

        public ClientResolver(
            Func<Type, IAPI> apiFactory,
            Func<EndPointType, T> serviceFactory,
            Func<EndPointType, IEndPointValidator> validatorFactory,
            EndPointInstance endPointInstance
            )
        {
            this.apiFactory = apiFactory;
            this.serviceFactory = serviceFactory;
            this.validatorFactory = validatorFactory;
            this.endPointInstance = endPointInstance;
        }

        /// <summary>
        /// Given a service interface (IOrdersService), we find an API (MyStore).
        /// Given the API, we find an EndPoint.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T ResolveClient()
        {
            T client = default(T);
            IAPI api = apiFactory(typeof(T));

            if (api == null)
                return client;

            foreach (IEndPointConfiguration endPoint in api.EndPoints)
            {
                IEndPointValidator validator = validatorFactory(endPoint.EndPointType);

                if (!validator.IsInterfaceAlive(endPoint))
                    continue;

                // must set CurrentEndPoint before calling serviceFactory
                endPointInstance.CurrentEndPoint = endPoint;
                client = serviceFactory(endPoint.EndPointType);

                if (client == null)
                    continue;
                
                break;
            }
            return client;
        }
    }
}
