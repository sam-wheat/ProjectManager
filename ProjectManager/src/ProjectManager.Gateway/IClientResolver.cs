using System;
using System.Collections.Generic;
using Autofac;
using ProjectManager.Core;

namespace ProjectManager.Gateway
{
    public interface IClientResolver
    {
        T ResolveClient<T>(ILifetimeScope container);
        void RegisterService(Type serviceType, IAPI api);
    }
}