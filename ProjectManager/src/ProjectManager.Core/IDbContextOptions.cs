using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjectManager.Core
{
    public interface IDbContextOptions
    {
        DbContextOptions Options { get; }
    }
}
