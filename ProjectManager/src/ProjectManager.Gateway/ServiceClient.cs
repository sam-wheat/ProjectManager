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
        private ILifetimeScope _container;

        public ServiceClient(ILifetimeScope container)
        {
            _container = container;
        }

        public IServiceCallWrapper<T> OfType<T>() where T : class, IDisposable
        {
            return new ServiceCallWrapper<T>(_container);
        }
    }

    public class ServiceCallWrapper<T> : IServiceCallWrapper<T> where T : class, IDisposable
    {
        private ILifetimeScope _container;
        private EndPointConfigFactory<T> _factory;

        internal ServiceCallWrapper(ILifetimeScope container)
        {
            _container = container;
            _factory = container.Resolve<EndPointConfigFactory<T>>();
        }

        public void Try(Action<T> method)
        {
            // consider try/catch/log/throw here

            using (var scope = _container.BeginLifetimeScope())
            {
                T client = scope.Resolve<T>();
                method(client);
            }
        }

        public TResult Try<TResult>(Func<T, TResult> method)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                T client = scope.Resolve<T>();
                return method(client);
            }
        }

        public async Task TryAsync(Func<T, Task> method)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                
                T client = scope.Resolve<T>();
                await method(client);
            }
        }

        public async Task<TResult> TryAsync<TResult>(Func<T, Task<TResult>> method)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                T client = scope.Resolve<T>();
                return await method(client);
            }
        }
    }
}
