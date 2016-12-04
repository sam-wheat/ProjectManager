using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ProjectManager.Model.Domain
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public virtual HashSet<Activity> Activities { get; set; }
        public virtual HashSet<Contact> Contacts { get; set; }
        public virtual HashSet<Project> Projects { get; set; }
        public virtual HashSet<Reminder> Reminders { get; set; }
    }
}
