using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Model.Domain
{
    public class DefaultContact
    {
        public int ProjectID { get; set; }
        public int ContactID { get; set; }

        public virtual Project Project { get; set; }
        public virtual Contact Contact { get; set; }
    }
}
