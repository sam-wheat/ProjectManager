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
        public static void Register(IEnumerable<EndPointConfiguration> endPoints, ContainerBuilder builder)
        {
            if (endPoints == null)
                return;

            if (builder == null)
                throw new ArgumentNullException("builder");

            endPoints = endPoints.Where(x => x.IsActive);

            if (endPoints.Any(x => string.IsNullOrEmpty(x.Name)))
                throw new Exception("One or more EndPointConfigurations has a blank name.  Name is required for all EndPointConfigurations");

            var dupes = endPoints.GroupBy(x => new { x.Name }).Where(x => x.Count() > 1);

            if (dupes.Any())
                throw new Exception($"Duplicate EndPointConfiguration found. EndPoint Name: {dupes.First().Key.Name}." + Environment.NewLine + "Each EndPoint must have a unique name.  Set the Active flag to false to bypass an EndPoint.");

            foreach (EndPointConfiguration endPoint in endPoints)
                builder.RegisterInstance(endPoint).Keyed<IEndPointConfiguration>(endPoint.Name).SingleInstance();
        }
    }
}
