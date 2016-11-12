using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManager.Core
{
    public interface IAPI
    {
        string API_Name { get; }
        IList<EndPointType> EndPointPreferences { get; }
    }
}
