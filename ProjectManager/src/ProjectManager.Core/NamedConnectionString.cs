using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManager.Core
{
    public class NamedConnectionString : INamedConnectionString
    {
        public string ConnectionString { get; set; }
    }
}
