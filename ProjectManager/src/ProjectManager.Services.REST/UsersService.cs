using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using ProjectManager.Model;
using ProjectManager.Domain;
using ProjectManager.Core;
using ProjectManager.Model.Domain;
using ProjectManager.Model.Presentation;

namespace ProjectManager.Services.REST
{
    public class UsersService : BaseService, IUsersService
    {
        public UsersService(Func<IEndPointConfiguration> endPointFactory) :base(endPointFactory)
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

        public async Task<IAsyncServiceResult<User>> GetUser(string userName, string password)
        {
            string json = await httpClient.GetStringAsync($"users/getuser?userName={userName}&password={password}");
            AsyncResult<User> result = JsonConvert.DeserializeObject<AsyncResult<User>>(json);
            return result;
        }

        public User[] GetUsers(bool activeOnly = true)
        {
            throw new NotImplementedException();
        }

        public async Task<IAsyncServiceResult> SaveUser(User user)
        {
            string json = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage msg = await httpClient.PostAsync("users/saveuser", content);
            AsyncResult result = new AsyncResult();
            return result;
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
