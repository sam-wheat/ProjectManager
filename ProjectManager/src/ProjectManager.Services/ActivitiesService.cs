using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using ProjectManager.Model;
using ProjectManager.BusinessLogic.Presentation;

namespace ProjectManager.BusinessLogic.Services
{
    public partial class DataServices
    {
        private void attachActivity(Activity activity)
        {
            if (activity.ID == 0)
                db.Activities.Add(activity);
            else
                db.AttachAsModfied(activity); 
        }

        private void deleteActivity(Activity activity)
        {
            if (db.Entry(activity).State == EntityState.Detached)
                db.AttachAsDeleted(activity);
            else
                db.Entry(activity).State = EntityState.Deleted;
        }

        private void deleteActivities(IEnumerable<Activity> activities)
        {
            activities.ToList().ForEach(a => deleteActivity(a));
        }

        public Activity GetActivity(int id)
        {
            return db.Activities.SingleOrDefault(x => x.ID == id);
        }

        public PresActivity[] SearchActivities(int userID, int projectID, int pageIndex, int pageSize, string sortKey, string sortDir, out int totalResultCount)
        {
            var query = db.Activities.Include("Project")
               .Where(x => (x.UserID == userID || userID == 0) && (x.ProjectID == projectID || projectID == 0));

            if (sortDir == "ASC")
            {
                if (sortKey == "Project")
                    query = query.OrderBy(x => x.Project.Name);
                else
                    query = query.OrderBy(x => x.Date);
            }
            else if (sortDir == "DESC")
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
                .Select(x => new PresActivity(x))
                .ToArray();
        }

        public Activity[] GetActivitiesForProject(int projectID)
        {
            return db.Activities.Where(x => x.ProjectID == projectID).OrderByDescending(x => x.Date).ToArray();
        }

        public PresActivity[] GetPresActivitiesForProject(int projectID)
        {
            return GetActivitiesForProject(projectID).Select(x => new PresActivity(x)).ToArray();
        }

        public int ModifyActivity(Activity activity)
        {
            throw new NotImplementedException();
        }

        public int DeleteActivity(Activity activity)
        {
            deleteActivity(activity);
            return db.SaveChanges();
        }

        public int DeleteActivityByID(int activityID)
        {
            int saveCount = 0;
            Activity a = GetActivity(activityID);
            
            if (a != null)
            {
                deleteActivity(a);
                saveCount = db.SaveChanges();
            }
            return saveCount;
        }

        public bool ValidateActivity(Activity activity, out string errorMsg)
        {
            errorMsg = "";
            bool result = true;

            if (string.IsNullOrEmpty(activity.Notes))
            {
                errorMsg = "Notes field cannot be blank.";
                return false;
            }
            return result;
        }
    }
}
