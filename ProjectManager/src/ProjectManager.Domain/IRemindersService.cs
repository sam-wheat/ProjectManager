using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ServiceModel;
using ProjectManager.Model.Domain;
using ProjectManager.Model.Presentation;

namespace ProjectManager.Domain
{
    public interface IRemindersService : IDisposable
    {
        [OperationContract]
        void DeleteReminders(IEnumerable<Reminder> reminders);
        [OperationContract]
        void DeleteReminder(Reminder reminder);
        [OperationContract]
        void AttachReminder(Reminder reminder);
        [OperationContract]
        Reminder GetReminder(int id);
        [OperationContract]
        Reminder[] GetRemindersForProject(int projectID);
        [OperationContract]
        PresReminder[] GetPresRemindersForProject(int projectID);
        [OperationContract]
        PresReminder[] GetActivePresReminders(int userID, int pageIndex, int pageSize, string sortKey, string sortDir, out int totalResultCount);
        [OperationContract]
        Reminder[] GetRemindersForFutureDays(int futureDays); // number of days into the future
        [OperationContract]
        Reminder[] GetRemindersForUser(int userID, bool activeOnly = true);
        [OperationContract]
        bool ValidateReminder(Reminder reminder, out string errorMsg);
        [OperationContract]
        int DeleteReminderAndSave(int reminderID);
        [OperationContract]
        int ToggleReminderCompletion(int reminderID);
    }
}
