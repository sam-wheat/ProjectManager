using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Model.Domain
{
    public class Reminder
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public Nullable<int> ProjectID { get; set; }
        public DateTime Date { get; set; }      // i.e. date when event is scheduled to happen
        public bool IsComplete { get; set; }
        public string Notes { get; set; }
        public virtual Project Project { get; set; }
        public virtual User User { get; set; }
    }
}
