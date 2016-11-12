﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectManager.Domain;
using Autofac;
using Autofac.Core;
using ProjectManager.Core;

namespace ProjectManager.Services
{
    public class IOCModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<Integration.DropAndRecreateInitializer>();
            builder.RegisterType<ProjectManagerDbContextOptions>()
                .As<IDbContextOptions>()
                .WithParameter(new ResolvedParameter(
                    (p, c) => p.ParameterType == typeof(InProcessEndPoint),
                    (p, c) => c.ResolveKeyed<InProcessEndPoint>(APIName.ProjectManager)
                ));

            builder.RegisterType<Db>().InstancePerLifetimeScope();  // One instance for all services that request a Db within a lifetimeScope
            builder.RegisterType<ActivitiesService>().As<IActivitiesService>();
            builder.RegisterType<ContactsService>().As<IContactsService>();
            builder.RegisterType<DefaultContactsService>().As<IDefaultContactsService>();
            builder.RegisterType<ProjectsService>().As<IProjectsService>();
            builder.RegisterType<RemindersService>().As<IRemindersService>();
            builder.RegisterType<UsersService>().Keyed<IUsersService>(EndPointType.InProcess);
            builder.RegisterType<DatabaseUtilitiesServicecs>().As<IDatabaseUtilitiesService>();
            
        }
    }
}
    