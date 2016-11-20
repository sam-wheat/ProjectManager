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
        private IClientResolver resolver;

        public ServiceClient(ILifetimeScope container, IClientResolver resolver)
        {
            this.container = container;
            this.resolver = resolver;
        }

        public void Try(Action<T> method)
        {
            using (var scope = container.BeginLifetimeScope())
            {
                T client = resolver.ResolveClient<T>(scope);
                method(client);
            }
        }

        public TResult Try<TResult>(Func<T, TResult> method)
        {
            using (var scope = container.BeginLifetimeScope())
            {
                T client = resolver.ResolveClient<T>(scope);
                return method(client);
            }
        }

        public async Task TryAsync(Func<T, Task> method)
        {
            using (var scope = container.BeginLifetimeScope())
            {
                T client = resolver.ResolveClient<T>(scope);
                await method(client);
            }
        }

        public async Task<TResult> TryAsync<TResult>(Func<T, Task<TResult>> method)
        {
            using (var scope = container.BeginLifetimeScope())
            {
                T client = resolver.ResolveClient<T>(scope);
                return await method(client);
            }
        }
    }
}
