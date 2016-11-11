using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using ProjectManager.Core;

//http://stackoverflow.com/questions/15421430/autofac-delegate-factory-using-func

namespace ProjectManager.Gateway
{
    public class ServiceClient : IServiceClient
    {
        private ILifetimeScope container;

        public ServiceClient(ILifetimeScope container)
        {
            this.container = container;
        }

        public IServiceCallWrapper<T> OfType<T>() where T : class, IDisposable
        {
            return new ServiceCallWrapper<T>(container);
        }
    }

    public class ServiceCallWrapper<T> : IServiceCallWrapper<T> where T : class, IDisposable
    {
        private ILifetimeScope _container;

        internal ServiceCallWrapper(ILifetimeScope container)
        {
            _container = container;
        }

        public void Try(Action<T> method)
        {

            using (var scope = _container.BeginLifetimeScope())
            {
                T client = ResolveClient(scope);
                method(client);
            }
        }

        public TResult Try<TResult>(Func<T, TResult> method)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                T client = ResolveClient(scope);
                return method(client);
            }
        }

        public async Task TryAsync(Func<T, Task> method)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                T client = ResolveClient(scope);
                await method(client);
            }
        }

        public async Task<TResult> TryAsync<TResult>(Func<T, Task<TResult>> method)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                T client = ResolveClient(scope);
                return await method(client);
            }
        }

        protected virtual T ResolveClient(ILifetimeScope container)
        {
            T client = default(T);

            if (false)                       //if (Utilities.VerifyNetworkConnectivity())
                client = container.ResolveKeyed<T>(EndPointType.InProcess);
            else
                client = container.ResolveKeyed<T>(EndPointType.REST);

            return client;
        }
    }
}
