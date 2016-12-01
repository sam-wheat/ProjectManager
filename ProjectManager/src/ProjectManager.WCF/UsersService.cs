using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ProjectManager.Core;
using ProjectManager.Domain;
using ProjectManager.Model.Domain;
using ProjectManager.Model.Presentation;

namespace ProjectManager.WCF
{
    public class UsersService : IUsersService
    {
        public UsersService()
        {
        }

        public int DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public int DeleteUserByID(int userID)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public async Task<IAsyncServiceResult<User>> GetUser(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public User[] GetUsers(bool activeOnly = true)
        {
            throw new NotImplementedException();
        }

        public async Task<IAsyncServiceResult> SaveUser(User user)
        {
            throw new NotImplementedException();
        }

        public PresUser[] SearchUsers(int pageIndex, int pageSize, string sortKey, string sortDir, out int totalResultCount)
        {
            throw new NotImplementedException();
        }

        public bool ValidateUser(User user, out string errorMsg)
        {
            throw new NotImplementedException();
        }

        public bool VerifyLogin(int userID, string password)
        {
            throw new NotImplementedException();
        }
    }
}