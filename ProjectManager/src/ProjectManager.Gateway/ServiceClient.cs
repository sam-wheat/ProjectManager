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
        private ClientResolver<T> resolver;

        public ServiceClient(ClientResolver<T> resolver)
        {
            this.resolver = resolver;
        }

        public void Try(Action<T> method)
        {
            using (T client = resolver.ResolveClient())
            {
                method(client);
            }
        }

        public TResult Try<TResult>(Func<T, TResult> method)
        {
            using (T client = resolver.ResolveClient())
            {
                return method(client);
            }
        }

        public async Task TryAsync(Func<T, Task> method)
        {
            using (T client = resolver.ResolveClient())
            {
                await method(client);
            }
        }

        public async Task<TResult> TryAsync<TResult>(Func<T, Task<TResult>> method)
        {
            using (T client = resolver.ResolveClient())
            {
                return await method(client);
            }
        }
    }
}
