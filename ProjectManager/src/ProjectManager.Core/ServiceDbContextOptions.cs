﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace ProjectManager.Core
{
    public abstract class ServiceDbContextOptions : IDbContextOptions
    {
        public DbContextOptions Options { get; private set; }

        public ServiceDbContextOptions(IEndPointConfiguration endPoint)
        {
            Configure(endPoint);
        }

        public virtual void Configure(IEndPointConfiguration endpoint)
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseSqlServer(endpoint.ConnectionString);
            Options = builder.Options;
        }
    }
}

