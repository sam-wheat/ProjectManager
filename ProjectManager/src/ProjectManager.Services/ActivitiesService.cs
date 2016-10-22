using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Model.Domain;
using ProjectManager.Model.Presentation;
using ProjectManager.Domain;

namespace ProjectManager.Services
{
    public class ActivitiesService : BaseService, IActivitiesService
    {
        

        public ActivitiesService(MyDbContextOptions options) : base(options)
        {

        }

        public void AttachActivity(Activity activity)
        {
            if (activity.ID == 0)
                db.Activities.Add(activity);
            else
                db.AttachAsModfied(activity); 
        }

        public void DeleteActivity(Activity activity)
        {
            if (db.Entry(activity).State == EntityState.Detached)
                db.AttachAsDeleted(activity);
            else
                db.Entry(activity).State = EntityState.Deleted;
        }

        private void deleteActivities(IEnumerable<Activity> activities)
        {
            activities.ToList().ForEach(a => DeleteActivity(a));
        }

        public Activity GetActivity(int id)
        {
            return db.Activities.SingleOrDefault(x => x.ID == id);
        }

        public PresActivity[] SearchActivities(int userID, int projectID, int pageIndex, int pageSize, string sortKey, string sortDir, out int totalResultCount)
        {
            var query = db.Activities.Include(x => x.Project)
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

        public int DeleteActivityAndSave(Activity activity)
        {
            DeleteActivity(activity);
            return db.SaveChanges();
        }

        public int DeleteActivities(IEnumerable<Activity> activities)
        {
            foreach (Activity activity in activities)
                DeleteActivity(activity);

            return db.SaveChanges();
        }

        public int DeleteActivityByID(int activityID)
        {
            int saveCount = 0;
            Activity a = GetActivity(activityID);
            
            if (a != null)
            {
                DeleteActivity(a);
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
