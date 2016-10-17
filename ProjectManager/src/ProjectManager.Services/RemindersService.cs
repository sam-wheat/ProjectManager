using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using ProjectManager.BusinessLogic.Presentation;
using ProjectManager.Model;

namespace ProjectManager.BusinessLogic.Services
{
    public partial class DataServices
    {
        public Reminder GetReminder(int id)
        {
            return db.Reminders.SingleOrDefault(x => x.ID == id);
        }

        public Reminder[] GetRemindersForProject(int projectID)
        {
            return db.Reminders
                .Where(x => x.ProjectID == projectID)
                .OrderByDescending(x => x.Date).ThenByDescending(x => x.ID)
                .ToArray();
        }

        public PresReminder[] GetPresRemindersForProject(int projectID)
        {
            return GetRemindersForProject(projectID)
                .Select(x => new PresReminder(x)).ToArray();
        }

        public PresReminder[] GetActivePresReminders(int userID, int pageIndex, int pageSize, string sortKey, string sortDir, out int totalResultCount)
        {
            var query = db.Reminders.Include("Project")
               .Where(x => x.UserID == userID && ! x.IsComplete);

            if(sortDir == "ASC")
            {
                if(sortKey == "Project")
                    query = query.OrderBy(x => x.Project.Name);
                else
                    query = query.OrderBy(x => x.Date);
            }
            else if(sortDir == "DESC")
            {
                if (sortKey == "Project")
                    query = query.OrderByDescending(x => x.Project.Name);
                else
                    query = query.OrderByDescending(x => x.Date);
            }
            else
                throw new Exception("sortDir not recognized: " + sortDir);

            totalResultCount = query.Count();

            query = query
               .Skip((pageIndex - 1) * pageSize)
               .Take(pageSize);

            return query
                .ToList()
                .Select(x => new PresReminder(x))
                .ToArray();
        }

        public Reminder[] GetRemindersForFutureDays(int futureDays)
        {
            throw new NotImplementedException();
        }

        public Reminder[] GetRemindersForUser(int userID, bool activeOnly = true)
        {
            throw new NotImplementedException();
        }
        
        public bool ValidateReminder(Reminder reminder, out string errorMsg)
        {
            errorMsg = "";
            bool result = true;

            if (string.IsNullOrEmpty(reminder.Notes))
            {
                errorMsg = "Notes cannot be blank.";
                return false;
            }
            return result;
        }

        private void attachReminder(Reminder reminder)
        {
            if (reminder.ID == 0)
                db.Reminders.Add(reminder);
            else
                db.AttachAsModfied(reminder);
        }

        private void deleteReminder(Reminder reminder)
        {
            if (db.Entry(reminder).State == EntityState.Detached)
                db.AttachAsDeleted(reminder);
            else
                db.Entry(reminder).State = EntityState.Deleted;
        }

        private void deleteReminders(IEnumerable<Reminder> reminders)
        {
            reminders.ToList().ForEach(r => deleteReminder(r));
        }

        public int DeleteReminder(int reminderID)
        {
            int saveCount = 0;
            Reminder r = GetReminder(reminderID);

            if (r != null)
            {
                deleteReminder(r);
                saveCount = db.SaveChanges();
            }
            return saveCount;
        }

        public int ToggleReminderCompletion(int reminderID)
        {
            int saveCount = 0;
            Reminder r = GetReminder(reminderID);

            if (r != null)
            {
                r.IsComplete = !r.IsComplete;
                attachReminder(r);
                saveCount = db.SaveChanges();
            }
            return saveCount;
        }
    }
}
