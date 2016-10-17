using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ServiceModel;
using ProjectManager.Model;

namespace ProjectManager.Domain
{
    public interface IProjectsService
    {
        [OperationContract]
        PresProject[] GetProjectsForUser(int userID, int pageIndex, int pageSize, string sort, string sortDir, Nullable<bool> IsComplete, string projectName, string notes, string tags, out int totalResultCount, int id = 0);
        [OperationContract]
        PresProject GetPresProject(int ID);
        [OperationContract]
        int SaveProject(PresProject presProject, IEnumerable<PresActivity> activities, IEnumerable<PresReminder> reminders, IEnumerable<PresDefaultContact> contacts, out int id, out string errorMsg);
        [OperationContract]
        int DeleteProject(int id);
    }
}
