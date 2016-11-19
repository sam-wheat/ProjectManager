﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManager.Core
{
    public interface IEndPointConfiguration
    {
        string Name { get; set; }
        string API_Name { get; set; }
        int Preference { get; set; }
        EndPointType EndPointType { get; set; }
        string ConnectionString { get; set; }
        Dictionary<string, string> Parameters { get; set; }
    }
}
