using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Model.Domain;

namespace ProjectManager.Model.Presentation
{
    public class PresReminder :BasePresentationModel 
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public Nullable<int> ProjectID { get; set; }
        public string ProjectName { get; set; }
        public DateTime Date { get; set; }      
        public bool IsComplete { get; set; }
        public string Notes { get; set; }

        public PresReminder()
        { 
        
        }

        public PresReminder(Reminder reminder)
        {
            ID = reminder.ID;
            UserID = reminder.UserID;
            ProjectID = reminder.ProjectID;
            ProjectName = reminder.Project == null ? "" : reminder.Project.Name;
            Date = reminder.Date;
            IsComplete = reminder.IsComplete;
            Notes = reminder.Notes;
        }

        public Reminder ToReminder()
        {
            return new Reminder
            {
                ID = this.ID,
                UserID = this.UserID,
                ProjectID = this.ProjectID,
                Date = this.Date,
                IsComplete = this.IsComplete,
                Notes = this.Notes
            };
        }
    }
}
