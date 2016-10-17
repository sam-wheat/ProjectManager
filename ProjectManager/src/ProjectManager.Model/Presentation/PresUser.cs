using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Model.Domain;

namespace ProjectManager.Model.Presentation
{
    public class PresUser : BasePresentationModel 
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public virtual HashSet<Activity> Activities { get; set; }
        public virtual HashSet<Contact> Contacts { get; set; }
        public virtual HashSet<Project> Projects { get; set; }
        public virtual HashSet<Reminder> Reminders { get; set; }
        
        public PresUser()
        { 
        
        }

        public PresUser(User user)
        {
            ID = user.ID;
            Name = user.Name;
            Password = user.Password;
            IsActive = user.IsActive;
        }

        public User ToUser()
        {
            return new User
            {
                ID = this.ID,
                Name = this.Name,
                Password = this.Password,
                IsActive = this.IsActive
            };
        }
    }
}
