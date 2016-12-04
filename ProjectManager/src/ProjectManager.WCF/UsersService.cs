using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.ServiceModel;
using ProjectManager.Core;
using ProjectManager.Domain;
using ProjectManager.Model.Domain;
using ProjectManager.Model.Presentation;

namespace ProjectManager.WCF
{
    public class UsersService : IUsersService
    {
        private IUsersService usersService;

        public UsersService(IClientResolver<IUsersService> usersServiceFactory)
        {
            usersService = usersServiceFactory.ResolveClient();
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
            return await usersService.GetUser(userName, password);
        }

        public User[] GetUsers(bool activeOnly = true)
        {
            throw new NotImplementedException();
        }

        
        public async Task<IAsyncServiceResult> SaveUser(User user)
        {
            return await usersService.SaveUser(user);
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