using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManager.Core
{
    public interface IEndPointCollection
    {
        string Collection_Name { get; }
        IList<IEndPointConfiguration> EndPoints { get; }
    }
}
