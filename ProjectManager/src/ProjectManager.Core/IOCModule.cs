using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;

namespace ProjectManager.Core
{
    public class IOCModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<NLogger>().As<ILogger>();
            builder.RegisterType<InProcessEndPoint>().As<IEndPointConfiguration>().SingleInstance();
        }
    }
}
