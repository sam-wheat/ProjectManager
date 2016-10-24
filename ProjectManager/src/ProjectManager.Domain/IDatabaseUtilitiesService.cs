using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManager.Domain
{
    public interface IDatabaseUtilitiesService : IDisposable
    {
        void CreateOrUpdateDatabase();
    }
}
