using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Autofac.Features;
using ProjectManager.Domain;
using ProjectManager.Core;

namespace ProjectManager.Services.REST
{
    public class IOCModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<UsersService>().Keyed<IUsersService>(EndPointType.REST);
        }
    }
}
