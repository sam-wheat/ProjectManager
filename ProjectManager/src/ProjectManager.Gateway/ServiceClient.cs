﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using ProjectManager.Core;

namespace ProjectManager.Gateway
{
    public class ServiceClient<T> : IServiceClient<T> where T : class, IDisposable
    {
        private ILifetimeScope container;

        public ServiceClient(ILifetimeScope container)
        {
            this.container = container;
        }

        public void Try(Action<T> method, params string[] endPointNames)
        {
            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                IClientResolver<T> resolver = scope.Resolve<IClientResolver<T>>();
                T client = resolver.ResolveClient(endPointNames);
                method(client);
            }
        }

        public TResult Try<TResult>(Func<T, TResult> method, params string[] endPointNames)
        {
            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                IClientResolver<T> resolver = scope.Resolve<IClientResolver<T>>();
                T client = resolver.ResolveClient(endPointNames);
                return method(client);
            }
        }

        public async Task TryAsync(Func<T, Task> method, params string[] endPointNames)
        {
            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                IClientResolver<T> resolver = scope.Resolve<IClientResolver<T>>();
                T client = resolver.ResolveClient(endPointNames);
                await method(client);
            }
        }

        public async Task<TResult> TryAsync<TResult>(Func<T, Task<TResult>> method, params string[] endPointNames)
        {
            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                IClientResolver<T> resolver = scope.Resolve<IClientResolver<T>>();
                T client = resolver.ResolveClient(endPointNames);
                return await method(client);
            }
        }
    }
}
