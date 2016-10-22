using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManager.Core
{
    public interface INamedConnectionString
    {
        string ConnectionString { get; set; }
    }
}
