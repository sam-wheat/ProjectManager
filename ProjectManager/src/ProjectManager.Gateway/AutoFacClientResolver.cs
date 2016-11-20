using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Options;
using Autofac;
using ProjectManager.Core;


namespace ProjectManager.Gateway
{
    public class AutoFacClientResolver : IClientResolver
    {
        private ContainerBuilder builder;
        public IEndPointConfiguration CurrentEndPoint { get; private set; }
        private Dictionary<string, IAPI> EndPointDict;

        public AutoFacClientResolver(ContainerBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException("builder");

            this.builder = builder;
            EndPointDict = new Dictionary<string, IAPI>();
        }

        public void RegisterEndPoints(IEnumerable<IEndPointConfiguration> endPoints)
        {
            if (endPoints == null)
                return;
            
            // not sure this needs to be done
            //foreach (IEndPointConfiguration endPoint in endPoints)
            //    builder.RegisterInstance(endPoint).Keyed<IEndPointConfiguration>(endPoint.Name).SingleInstance();

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
            builder.RegisterInstance<IAPI>(api).Keyed<IAPI>(serviceType);
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

            if (!container.IsRegisteredWithKey<IAPI>(typeofT))
                return client;

            IAPI api = container.ResolveKeyed<IAPI>(typeofT);

            foreach (IEndPointConfiguration endPoint in api.EndPoints)
            {
                if (!container.IsRegisteredWithKey<T>(endPoint.EndPointType))
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
