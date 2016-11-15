using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using ProjectManager.Core;

namespace ProjectManager.Services.REST
{
    public abstract class BaseService : IDisposable
    {
        public HttpClient httpClient { get; private set; }

        public BaseService(IEndPointConfiguration endPoint)
        {
            httpClient = new HttpClient { BaseAddress = new Uri(endPoint.ConnectionString) };
        }

        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    disposed = true;
                    httpClient.Dispose();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
