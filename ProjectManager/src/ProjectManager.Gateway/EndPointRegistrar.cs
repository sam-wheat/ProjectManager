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
    public static class EndPointRegistrar
    {
        public static void Register(List<EndPointConfigurationTemplate> templates, ContainerBuilder builder)
        {
            if (templates == null)
                return;

            var dupes = templates.GroupBy(x => new { x.API_Name, x.EndPointType }).Where(x => x.Count() > 1);

            if (dupes.Any())
                throw new Exception($"Duplicate EndPointConfiguration found.  API_Name: {dupes.First().Key.API_Name}, EndPointType: {dupes.First().Key.EndPointType}.");

            foreach (EndPointConfigurationTemplate template in templates)
                RegisterEndPoint(template, builder);
                
        }

        private static void RegisterEndPoint(EndPointConfigurationTemplate template, ContainerBuilder builder)
        {
            IEndPointConfiguration config;

            switch (template.EndPointType)
            {
                case EndPointType.InProcess:
                    config = new InProcessEndPoint();
                    builder.RegisterInstance(config).As<InProcessEndPoint>().SingleInstance();
                    break;
                case EndPointType.REST:
                    config = new RESTEndPoint();
                    builder.RegisterInstance(config).As<RESTEndPoint>().SingleInstance();
                    break;
                case EndPointType.WCF:
                    config = new WCFEndPoint();
                    builder.RegisterInstance(config).As<WCFEndPoint>().SingleInstance();
                    break;
                default:
                    throw new Exception("");
            }

            config.API_Name = template.API_Name;
            config.ConnectionString = template.ConnectionString;
            config.Parameters = template.Parameters;
        }
    }
}
