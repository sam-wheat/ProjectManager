﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ServiceModel;
using ProjectManager.Model.Domain;
using ProjectManager.Model.Presentation;

namespace ProjectManager.Domain
{
    public interface IActivitiesService : IDisposable
    {
        [OperationContract]
        void DeleteActivity(Activity activity);
        [OperationContract]
        void AttachActivity(Activity activity);
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
        int DeleteActivityAndSave(Activity activity);
        [OperationContract]
        int DeleteActivities(IEnumerable<Activity> activities);
        [OperationContract]
        int DeleteActivityByID(int activityID);
        [OperationContract]
        bool ValidateActivity(Activity activity, out string errorMsg);
    }
}
