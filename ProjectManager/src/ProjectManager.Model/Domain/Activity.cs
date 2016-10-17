using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Model.Domain
{
    public class Activity
    {
        public long ID { get; set; }
        public int UserID { get; set; }
        public Nullable<int> ProjectID { get; set; }
        public Nullable<int> ContactID { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public string Media { get; set; } // i.e. Phone, text, email etc.
        public string Tags { get; set; }
        
        public virtual Project Project { get; set; }
        public virtual Contact Contact { get; set; }
        public virtual User User { get; set; }
    }
}
