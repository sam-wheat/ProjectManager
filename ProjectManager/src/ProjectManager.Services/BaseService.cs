using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Domain;

namespace ProjectManager.Services
{
    public abstract class BaseService : IDisposable
    {
        internal Db db;
        public abstract APIName APIName { get; }

        public BaseService(Db db)
        {
            this.db = db;
        }

        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    disposed = true;
                    db.Dispose();
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
