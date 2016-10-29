using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManager.Core
{
    public interface IEndPointConfiguration
    {
        string API_Name { get; set; }
        string ConnectionString { get; set; }
        Dictionary<string, string> Parameters { get; set; }
    }
}
