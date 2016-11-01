using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Core;

namespace ProjectManager.Services
{
    public class SQLServerDbContextOptions
    {
        public DbContextOptions Options { get; private set; }

        public SQLServerDbContextOptions(InProcessEndPoint endpoint)
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseSqlServer(endpoint.ConnectionString);
            Options = builder.Options;
        }
    }
}

