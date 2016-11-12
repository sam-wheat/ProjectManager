using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using ProjectManager.Core;

namespace ProjectManager.Gateway
{
    public class IOCModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<ServiceClient>().As<IServiceClient>();
            builder.RegisterGeneric(typeof(ServiceCallWrapper<>)).As(typeof(IServiceCallWrapper<>)).InstancePerLifetimeScope();
        }
    }
}
