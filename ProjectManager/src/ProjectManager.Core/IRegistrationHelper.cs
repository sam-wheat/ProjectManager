using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManager.Core
{
    public interface IRegistrationHelper
    {
        void RegisterEndPoints(IEnumerable<IEndPointConfiguration> endPoints);
        void RegisterAPI(Type serviceType, string api_name);
        void RegisterAPI(Type serviceType, IAPI api);
    }
}
