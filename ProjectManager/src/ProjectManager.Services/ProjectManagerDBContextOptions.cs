using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectManager.Core;

namespace ProjectManager.Services
{
    public class ProjectManagerDbContextOptions : ServiceDbContextOptions
    {
        public ProjectManagerDbContextOptions(InProcessEndPoint endPoint) : base(endPoint)
        {

        }
    }
}
