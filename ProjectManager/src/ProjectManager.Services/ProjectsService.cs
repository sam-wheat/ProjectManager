using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Model.Domain;
using ProjectManager.Model.Presentation;
using ProjectManager.Domain;
using ProjectManager.Core;

namespace ProjectManager.Services
{
    public class ProjectsService : BaseService, IProjectsService
    {
        private IActivitiesService ActivitiesService;
        private IRemindersService RemindersService;
        private IDefaultContactsService DefaultContactsService;

        public ProjectsService(Db db, 
            IClientResolver<IActivitiesService> actvitiesServiceFactory, 
            IClientResolver<IRemindersService> remindersServiceFactory, 
            IClientResolver<IDefaultContactsService> defaultContactsServiceFactory) : base(db)
        {
            ActivitiesService = actvitiesServiceFactory.ResolveClient();
            RemindersService = remindersServiceFactory.ResolveClient();
            DefaultContactsService = defaultContactsServiceFactory.ResolveClient();
        }

        private bool ValidateProjectHeader(Project project, out string errorMsg)
        {
            errorMsg = "";
            
            if(String.IsNullOrEmpty(project.Name))
            {
                errorMsg = "Project name can not be blank.";
                return false;
            }
            return true;

        }

        private void SaveProjectActivities(IEnumerable<PresActivity> activities, Project project, out string errorMsg)
        {
            errorMsg = "";
            IEnumerable<Activity> deletes = activities.Where(x => x.IsDeleted && x.ID > 0).Select(x => x.ToActivity());

            if (deletes.Any())
                ActivitiesService.DeleteActivities(deletes);

            foreach (PresActivity presActivity in activities.Where(x => x.IsDeleted || x.IsModified))
            {
                Activity a = presActivity.ToActivity();

                if (presActivity.IsDeleted)
                {
                    if (a.ID > 0)
                        ActivitiesService.DeleteActivity(a);
                }
                else
                {
                    if (ActivitiesService.ValidateActivity(a, out errorMsg))
                    {
                        if (project.Activities == null)
                            project.Activities = new HashSet<Activity>();

                        a.Project = project;
                        project.Activities.Add(a);
                        a.User = null;
                        ActivitiesService.AttachActivity(a);
                    }
                    else
                        break;
                }
            }
        }

        private void SaveProjectReminders(IEnumerable<PresReminder> reminders, Project project, out string errorMsg)
        {
            errorMsg = "";

            foreach (PresReminder presReminder in reminders.Where(x => x.IsDeleted || x.IsModified))
            {
                Reminder r = presReminder.ToReminder();

                if (presReminder.IsDeleted)
                {
                    if (r.ID > 0)
                        RemindersService.DeleteReminder(r);
                }
                else
                {
                    if (RemindersService.ValidateReminder(r, out errorMsg))
                    {
                        if (project.Reminders == null)
                            project.Reminders = new HashSet<Reminder>();

                        r.Project = project;
                        project.Reminders.Add(r);
                        r.User = null;
                        RemindersService.AttachReminder(r);
                    }
                    else
                        break;
                }
            }
        }

        private void SaveProjectDefaultContacts(IEnumerable<PresDefaultContact> contacts, Project project, out string errorMsg)
        {
            errorMsg = "";
            
            // We select rows where the entity is deleted or is modified but not both.  If an entity is both deleted and modified
            // it means the user added and deleted the entity in the same session without ever saving it to disk. We can therefore
            // just ignore it.

            foreach (PresDefaultContact presContact in contacts.Where(x => x.IsDeleted ^ x.IsModified))
            {
                DefaultContact c = presContact.ToDefaultContact();

                if (presContact.IsDeleted)
                {
                    DefaultContactsService.DeleteDefaultContact(c);
                }
                else
                {
                    if (project.DefaultContacts == null)
                        project.DefaultContacts = new HashSet<DefaultContact>();
                    
                    project.DefaultContacts.Add(c);
                    DefaultContactsService.AttachDefaultContact(c);
                }
            }
        }

        public PresProject[] GetProjectsForUser(int userID, int pageIndex, int pageSize, string sort, string sortDir, Nullable<bool> IsComplete, string projectName, string notes, string tags, out int totalResultCount, int id = 0)
        {
            if (string.IsNullOrEmpty(projectName))
                projectName = null;

            if (string.IsNullOrEmpty(tags))
                tags = null;

            if (string.IsNullOrEmpty(notes))
                notes = null;
            else
                notes = notes.ToUpper();

            var tmp = (from p in db.Projects.Include(x => x.Activities)
                       where p.UserID == userID
                       && (IsComplete == null || p.IsComplete == IsComplete.Value)
                       && (id == 0 || p.ID == id)
                       && (projectName == null || p.Name.ToUpper() == projectName.ToUpper())
                       && (tags == null || p.Tags.ToUpper() == tags.ToUpper())
                       && (notes == null || p.Activities.Any(x => x.Notes.ToUpper().Contains(notes)))
                       let lastActvity = p.Activities.OrderByDescending(x => x.Date).FirstOrDefault()
                       let nextReminder = p.Reminders.Where(x => !x.IsComplete).OrderBy(x => x.Date).FirstOrDefault()

                       select new
                       {
                           Project = p,
                           UserName = p.User.Name,
                           LastActivity = lastActvity,
                           NextReminder = nextReminder
                       });
            
            if (sortDir == "ASC")
            {
                switch (sort)
                { 
                    case "Name":
                        tmp = tmp.OrderBy(x => x.Project.Name);
                        break;
                    case "DueDate":
                        tmp = tmp.OrderBy(x => x.Project.DueDate);
                        break;
                    case "IsComplete":
                        tmp = tmp.OrderBy(x => x.Project.IsComplete).ThenBy(x => x.Project.CompletionDate);
                        break;
                    case "LastActivity":
                        tmp = tmp.OrderBy(x => x.LastActivity.Date);
                        break;
                    case "NextReminder":
                        tmp = tmp.OrderBy(x => x.NextReminder.Date);
                        break;
                    default:
                        throw new Exception("Sort not recognized: " + sort);
                }
            }
            else if (sortDir == "DESC")
            {
                switch (sort)
                {
                    case "Name":
                        tmp = tmp.OrderByDescending(x => x.Project.Name);
                        break;
                    case "DueDate":
                        tmp = tmp.OrderByDescending(x => x.Project.DueDate);
                        break;
                    case "IsComplete":
                        tmp = tmp.OrderByDescending(x => x.Project.IsComplete).ThenByDescending(x => x.Project.CompletionDate);
                        break;
                    case "LastActivity":
                        tmp = tmp.OrderByDescending(x => x.LastActivity.Date);
                        break;
                    case "NextReminder":
                        tmp = tmp.OrderByDescending(x => x.NextReminder.Date);
                        break;
                    default:
                        throw new Exception("Sort not recognized: " + sort);
                }
            }
            else
                throw new Exception("sortDir not recognized: " + sortDir);
            
            var tmpList = tmp.ToList();
            totalResultCount = tmpList.Count;

            var result = from p in tmpList.Skip((pageIndex - 1) * pageSize).Take(pageSize)
                         select new PresProject
                         {
                             ID = p.Project.ID,
                             ProjectDate = p.Project.ProjectDate,
                             Name = p.Project.Name,
                             UserID = p.Project.UserID,
                             UserName = p.UserName,
                             IsComplete = p.Project.IsComplete,
                             Tags = p.Project.Tags,
                             DueDate = p.Project.DueDate,
                             CompletionDate = p.Project.CompletionDate,
                             LastActivity = p.LastActivity == null ? "N/A" : (p.LastActivity.Date.ToString("MM/dd/yy") + " - " + p.LastActivity.Notes),
                             NextReminder = p.NextReminder == null ? "N/A" : (p.NextReminder.Date.ToString("MM/dd/yy") + " - " + p.NextReminder.Notes)
                         };


            return result.ToArray();
        }

        public PresProject GetPresProject(int ID)
        {
            return new PresProject(db.Projects.Single(x => x.ID == ID));
        }

        public int SaveProject(PresProject presProject, IEnumerable<PresActivity> activities, IEnumerable<PresReminder> reminders, IEnumerable<PresDefaultContact> contacts, out int id, out string errorMsg)
        {
            errorMsg = "";
            id = 0;
            int saveCount = 0;

            Project project = presProject.ToProject();

            if (!ValidateProjectHeader(project, out errorMsg))
                return saveCount;

            if(String.IsNullOrEmpty(errorMsg)  && activities != null)
                SaveProjectActivities(activities, project, out errorMsg);

            if(String.IsNullOrEmpty(errorMsg) && reminders != null)
                SaveProjectReminders(reminders, project, out errorMsg);

            if(string.IsNullOrEmpty(errorMsg) && contacts != null)
                SaveProjectDefaultContacts(contacts, project, out errorMsg);

            if (string.IsNullOrEmpty(errorMsg))
            {
                if (project.ID == 0)
                    db.Projects.Add(project);
                else
                    db.AttachAsModfied(project);

                saveCount = db.SaveChanges();
                id = project.ID;
            }
            return saveCount;
        }

        private void deleteProject(int id)
        {
            Project p = db.Projects
                .Include(x => x.Activities)
                .Include(x => x.DefaultContacts)
                .Include(x => x.Reminders)
                .SingleOrDefault(x => x.ID == id);

            ActivitiesService.DeleteActivities(p.Activities);

            DefaultContactsService.DeleteDefaultContacts(p.DefaultContacts);

            RemindersService.DeleteReminders(p.Reminders);

            db.Entry(p).State = EntityState.Deleted;       
        }

        public int DeleteProject(int id)
        {
            deleteProject(id);
            return db.SaveChanges();
        }
    }
}
