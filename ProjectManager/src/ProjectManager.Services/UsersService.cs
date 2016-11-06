using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Core;
using ProjectManager.Model.Domain;
using ProjectManager.Model.Presentation;
using ProjectManager.Domain;

namespace ProjectManager.Services
{
    public class UsersService : BaseService, IUsersService
    {
        private IContactsService ContactsService;
        private IProjectsService ProjectsService;

        public UsersService(Db db, IProjectsService projectsService, IContactsService contactsService) : base(db)
        {
            ContactsService = contactsService;
            ProjectsService = projectsService;
        }

        public async Task<IAsyncServiceResult<User>> GetUser(string userName, string password)
        {
            IAsyncServiceResult<User> result = new AsyncResult<User>();
            result.Data = await db.Users.SingleOrDefaultAsync(x => x.Name == userName && x.Password == password && x.IsActive);
            result.Success = true;
            return result;
        }

        public async Task<IAsyncServiceResult> SaveUser(User user)
        {
            IAsyncServiceResult result = new AsyncResult();
            string errorMsg = "";

            if (!ValidateUser(user, out errorMsg))
            {
                result.ErrorMessage = errorMsg;
                return result;
            }


            if (user.ID == 0)
                db.Users.Add(user);
            else
                db.AttachAsModfied(user);

            await db.SaveChangesAsync();
            result.Result = user.ID;
            result.Success = true;
            return result;
        }

        public bool ValidateUser(User user, out string errorMsg)
        {
            errorMsg = "";

            if (string.IsNullOrEmpty(user.Name))
                errorMsg = "User name cannot be blank.";
            else if (string.IsNullOrEmpty(user.Password))
                errorMsg = "User password cannot be blank.";
            else if (db.Users.Any(x => x.Name == user.Name && x.ID != user.ID))
                errorMsg = "A user named " + user.Name + " already exists.  Choose another name.";

            return string.IsNullOrEmpty(errorMsg);
        }

        public int DeleteUser(User user)
        {
            db.Projects.Where(x => x.UserID == user.ID).ToList().ForEach(x => ProjectsService.DeleteProject(x.ID));
            db.Contacts.Where(x => x.UserID == user.ID).ToList().ForEach(x => ContactsService.DeleteContact(x));
            db.Users.Remove(user);
            return db.SaveChanges();
        }

        public int DeleteUserByID(int userID)
        {
            int saveCount = 0;
            User user = db.Users.SingleOrDefault(x => x.ID == userID);
         
            if (user != null)
                saveCount = DeleteUser(user);
            
            return saveCount;
        }

        public User[] GetUsers(bool activeOnly = true)
        {
            return db.Users.Where(x => x.IsActive || !activeOnly).ToArray();
        }

        public PresUser[] SearchUsers(int pageIndex, int pageSize, string sortKey, string sortDir, out int totalResultCount)
        {
            var query = db.Users.Where(x => true);

            if (sortDir == "ASC")
            {
                if (sortKey == "Name")
                    query = query.OrderBy(x => x.Name);
                else
                    query = query.OrderBy(x => x.IsActive).ThenBy(x => x.Name);
            }
            else if (sortDir == "DESC")
            {
                if (sortKey == "Name")
                    query = query.OrderByDescending(x => x.Name);
                else
                    query = query.OrderByDescending(x => x.IsActive).ThenByDescending(x => x.Name);
            }
            else
                throw new Exception("sortDir not recognized: " + sortDir);

            totalResultCount = query.Count();

            query = query
               .Skip((pageIndex - 1) * pageSize)
               .Take(pageSize);

            return query
                .ToList()
                .Select(x => new PresUser(x))
                .ToArray();
        }


        public bool VerifyLogin(int userID, string password)
        {
            return db.Users.Any(x => x.ID == userID && x.Password == password && x.IsActive);
        }
    }
}
