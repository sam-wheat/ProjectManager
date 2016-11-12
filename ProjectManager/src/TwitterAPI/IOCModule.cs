using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using TwitterAPI.Services;
using TwitterAPI.Domain;

namespace TwitterAPI
{
    public class IOCModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            
            builder.RegisterType<TweetService>().As<ITwitter>();
        }
    }
}
