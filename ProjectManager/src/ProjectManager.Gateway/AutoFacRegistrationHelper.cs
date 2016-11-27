using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Options;
using Autofac;
using Autofac.Features.OwnedInstances;
using ProjectManager.Core;


namespace ProjectManager.Gateway
{
    public class AutofacRegistrationHelper : IRegistrationHelper
    {
        private ContainerBuilder builder;
        private Dictionary<string, IAPI> EndPointDict;

        public AutofacRegistrationHelper(ContainerBuilder builder)
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
            
            // Do not register endpoints with the container.  A list of endpoints is available
            // when an API is resolved.
            
            foreach (var api in endPoints.GroupBy(x => x.API_Name))
                EndPointDict.Add(api.Key, new API(api.Key, api.ToList()));
        }

        /// <summary>
        /// Registers a service.  Call RegisterEndPoints before calling this method.
        /// </summary>
        /// <typeparam name="TService">Type of service i.e. OrdersService</typeparam>
        /// <typeparam name="TInterface">Interface of service i.e. IOrdersService</typeparam>
        /// <param name="endPointType">Type of client that will access this service i.e. REST, InProcess, WCF</param>
        /// <param name="apiName">Name of API that is composed by this service i.e. MyStore</param>
        public void RegisterService<TService, TInterface>(EndPointType endPointType, string apiName)
        {
            RegisterAPI(typeof(TInterface), apiName);
            builder.RegisterType<TService>().Keyed<TInterface>(endPointType);
            builder.Register<Func<EndPointType, TInterface>>(c => { IComponentContext cxt = c.Resolve<IComponentContext>(); return ep => cxt.ResolveKeyed<TInterface>(ep); });
        }

        /// <summary>
        /// Register API by name using service interface as key.  This method finds an instance of the API and calles RegisterAPI using that instance.
        /// </summary>
        /// <param name="serviceInterface"></param>
        /// <param name="api_name"></param>
        private void RegisterAPI(Type serviceInterface, string api_name)
        {
            IAPI api;
            EndPointDict.TryGetValue(api_name, out api);

            if (api == null)
                throw new Exception($"An API named {api_name} was not found.  Call the RegisterEndPoints method before calling RegisterService with an API name. Also check your spelling.");

            builder.RegisterInstance<IAPI>(api).Keyed<IAPI>(serviceInterface);
        }
    }
}
