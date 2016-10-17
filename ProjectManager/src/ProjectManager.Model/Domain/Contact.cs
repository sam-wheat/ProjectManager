using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Model.Domain
{
    public class Contact 
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public virtual User User { get; set; }
        public virtual HashSet<DefaultContact> DefaultContacts { get; set; }
    }
}
