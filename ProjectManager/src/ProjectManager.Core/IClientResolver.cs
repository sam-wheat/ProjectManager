using System;
using System.Collections.Generic;
using Autofac;
using ProjectManager.Core;

namespace ProjectManager.Core
{
    public interface IClientResolver
    {
        IEndPointConfiguration CurrentEndPoint { get; }
        void RegisterEndPoints(IEnumerable<IEndPointConfiguration> endPoints);
        void RegisterAPI(Type serviceType, string api_name);
        T ResolveClient<T>(ILifetimeScope container);
    }
}