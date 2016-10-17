using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Model.Domain
{
    public class Project
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public string Tags { get; set; }
        public DateTime ProjectDate { get; set; }
        public DateTime DueDate { get; set; }
        public Nullable<DateTime> CompletionDate { get; set; }

        public virtual HashSet<DefaultContact> DefaultContacts { get; set; }
        public virtual HashSet<Activity> Activities { get; set; }
        public virtual HashSet<Reminder> Reminders { get; set; }
        public virtual User User { get; set; }

        public Project()
        {
        }
    }
}
