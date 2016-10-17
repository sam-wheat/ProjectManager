using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ServiceModel;
using ProjectManager.Model;

namespace ProjectManager.Domain
{
    public interface IActivitiesService
    {
        [OperationContract]
        Activity GetActivity(int id);
        [OperationContract]
        PresActivity[] SearchActivities(int userID, int projectID, int pageIndex, int pageSize, string sortKey, string sortDir, out int totalResultCount);
        [OperationContract]
        Activity[] GetActivitiesForProject(int projectID);
        [OperationContract]
        PresActivity[] GetPresActivitiesForProject(int projectID);
        [OperationContract]
        int ModifyActivity(Activity activity);
        [OperationContract]
        int DeleteActivity(Activity activity);
        [OperationContract]
        int DeleteActivityByID(int activityID);
        [OperationContract]
        bool ValidateActivity(Activity activity, out string errorMsg);
    }
}
