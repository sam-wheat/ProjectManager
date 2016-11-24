using System;
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

        public void Try(Action<T> method)
        {
            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                IClientResolver<T> resolver = scope.Resolve<IClientResolver<T>>();
                T client = resolver.ResolveClient();
                method(client);
            }
        }

        public TResult Try<TResult>(Func<T, TResult> method)
        {
            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                IClientResolver<T> resolver = scope.Resolve<IClientResolver<T>>();
                T client = resolver.ResolveClient();
                return method(client);
            }
        }

        public async Task TryAsync(Func<T, Task> method)
        {
            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                IClientResolver<T> resolver = scope.Resolve<IClientResolver<T>>();
                T client = resolver.ResolveClient();
                await method(client);
            }
        }

        public async Task<TResult> TryAsync<TResult>(Func<T, Task<TResult>> method)
        {
            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                IClientResolver<T> resolver = scope.Resolve<IClientResolver<T>>();
                T client = resolver.ResolveClient();
                return await method(client);
            }
        }
    }
}
