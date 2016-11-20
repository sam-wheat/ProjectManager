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
            builder.RegisterGeneric(typeof(ServiceClient<>)).As(typeof(IServiceClient<>)).InstancePerLifetimeScope();
            builder.RegisterType<InProcessEndPointValidator>().Keyed<IEndPointValidator>(EndPointType.InProcess);
            builder.RegisterType<HttpEndPointValidator>().Keyed<IEndPointValidator>(EndPointType.WCF);
            builder.RegisterType<HttpEndPointValidator>().Keyed<IEndPointValidator>(EndPointType.REST);
        }
    }
}
