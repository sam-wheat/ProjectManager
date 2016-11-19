using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using ProjectManager.Core;

//http://stackoverflow.com/questions/15421430/autofac-delegate-factory-using-func

namespace ProjectManager.Gateway
{
    //public class ServiceClient : IServiceClient
    //{
    //    private ILifetimeScope container;

    //    public ServiceClient(ILifetimeScope container)
    //    {
    //        this.container = container;
    //    }

    //    public IServiceCallWrapper<T> OfType<T>() where T : class, IDisposable
    //    {
    //        return container.Resolve<IServiceCallWrapper<T>>();
    //    }
    //}

    public abstract class ServiceGateway<T> : IServiceGateway<T> where T : class, IDisposable
    {
        private ILifetimeScope container;
        private IClientResolver resolver;

        public ServiceGateway(ILifetimeScope container, IClientResolver resolver)
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
