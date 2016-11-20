﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectManager.Core;
using Autofac;

namespace ProjectManager.Gateway
{
    public class ClientResolver : IClientResolver
    {
        public IEndPointConfiguration CurrentEndPoint { get; private set; }
        private Dictionary<Type, IAPI> APIDict;
        private Dictionary<string, IAPI> EndPointDict;

        public ClientResolver()
        {
            APIDict = new Dictionary<Type, IAPI>();
            EndPointDict = new Dictionary<string, IAPI>();
        }

        public void RegisterEndPoints(IEnumerable<IEndPointConfiguration> endPoints)
        {
            foreach (var api in endPoints.GroupBy(x => x.API_Name))
                EndPointDict.Add(api.Key, new API(api.Key, api.ToList()));
        }

        public void RegisterAPI(Type serviceType, string api_name)
        {
            IAPI api;
            EndPointDict.TryGetValue(api_name, out api);

            if (api == null)
                throw new Exception($"An API named {api_name} was not found.  Call the RegisterEndPoints method before calling RegisterService with an API name. Also check your spelling.");

            RegisterAPI(serviceType, api);
        }

        public void RegisterAPI(Type serviceType, IAPI api)
        {
            APIDict[serviceType] = api;
        }

        /// <summary>
        /// Given a service interface (IOrdersService), we find an API (MyStore).
        /// Given the API, we find an EndPoint.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="container"></param>
        /// <returns></returns>
        public virtual T ResolveClient<T>(ILifetimeScope container)
        {
            T client = default(T);
            var typeofT = typeof(T);

            IAPI api;
            APIDict.TryGetValue(typeofT, out api); // Get the API that is registered to the client interface we are resolving

            if (api == null)
                return client;  // client is not registered to any API

            foreach (IEndPointConfiguration endPoint in api.EndPoints)
            {
                if(! container.IsRegisteredWithKey<T>(endPoint.EndPointType))
                    continue;

                CurrentEndPoint = endPoint;
                client = container.ResolveKeyed<T>(endPoint.EndPointType);
                IEndPointValidator validator = container.ResolveKeyed<IEndPointValidator>(endPoint.EndPointType);

                if (!validator.IsInterfaceAlive(endPoint))
                    continue;
        
                break;
            }
            return client;
        }
    }
}
