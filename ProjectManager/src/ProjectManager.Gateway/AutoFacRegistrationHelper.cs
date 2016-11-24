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
    public class AutoFacRegistrationHelper : IRegistrationHelper
    {
        private ContainerBuilder builder;
        public IEndPointConfiguration CurrentEndPoint { get; private set; }
        private Dictionary<string, IAPI> EndPointDict;

        public AutoFacRegistrationHelper(ContainerBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException("builder");

            this.builder = builder;
            EndPointDict = new Dictionary<string, IAPI>();
            builder.Register<Func<IEndPointConfiguration>>(c => { IComponentContext cxt = c.Resolve<IComponentContext>(); return () => cxt.Resolve<EndPointInstance>().CurrentEndPoint; });
            builder.Register<Func<Type, IAPI>>(c => { IComponentContext cxt = c.Resolve<IComponentContext>(); return t => cxt.ResolveKeyed<IAPI>(t); });
            builder.Register<Func<EndPointType, IEndPointValidator>>(c => { IComponentContext cxt = c.Resolve<IComponentContext>(); return ep => cxt.ResolveKeyed<IEndPointValidator>(ep); });
            builder.RegisterType<EndPointInstance>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(ClientResolver<>)).As(typeof(IClientResolver<>));
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

        public AutoFacServiceRegistrationHelper<TService> RegisterService<TService>()
        {
            return new AutoFacServiceRegistrationHelper<TService>(builder);
        }
    }

    public class AutoFacServiceRegistrationHelper<TService>
    {
        private ContainerBuilder builder;

        public AutoFacServiceRegistrationHelper(ContainerBuilder builder)
        {
            this.builder = builder;
        }

        public void Keyed<TInterface>(EndPointType endPointType)
        {
            builder.RegisterType<TService>().Keyed<TInterface>(endPointType);
            builder.Register<Func<EndPointType, TInterface>>(c => { IComponentContext cxt = c.Resolve<IComponentContext>(); return ep => cxt.ResolveKeyed<TInterface>(ep); });
        }
    }
}
