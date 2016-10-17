using System;
using System.Collections.Generic;
using System.Linq;
using ProjectManager.Model;
using ProjectManager.Model.Domain;

namespace ProjectManager.Model.Presentation
{
    public class PresProject : BasePresentationModel
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public string Tags { get; set; }
        public DateTime ProjectDate { get; set; }
        public DateTime DueDate { get; set; }
        public Nullable<DateTime> CompletionDate { get; set; }
        public string LastActivity { get; set; }  // Content of most recent Activity
        public string NextReminder { get; set; }  // Content of the nearest Reminder
        public string UserName { get; set; }

        public PresProject()
        {
        
        }

        public PresProject(Project project)
        {
            ID = project.ID;
            UserID = project.UserID;
            Name = project.Name;
            IsComplete = project.IsComplete;
            Tags = project.Tags;
            ProjectDate = project.ProjectDate;
            DueDate = project.DueDate;
            CompletionDate = project.CompletionDate;
        }

        public Project ToProject()
        {
            return new Project
            {
                ID = this.ID,
                UserID = this.UserID,
                Name = this.Name,
                IsComplete = this.IsComplete,
                Tags = this.Tags,
                ProjectDate = this.ProjectDate,
                DueDate = this.DueDate,
                CompletionDate = this.CompletionDate
            };
        }
    }
}