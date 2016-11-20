using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Model;
using ProjectManager.Services;
using ProjectManager.Core;
using ProjectManager.Domain;
using ProjectManager.Model.Domain;
using ProjectManager.Model.Presentation;


// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectManager.API.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : BaseController, IUsersService
    {
        private IServiceClient<IUsersService> usersService;

        public UsersController(IServiceClient<IUsersService> userService) 
        {
            this.usersService = usersService;
        }

        [HttpPost][Route("DeleteUser")]
        public int DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("DeleteUserByID")]
        public int DeleteUserByID(int userID)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("GetUser")]
        public async Task<IAsyncServiceResult<User>> GetUser(string userName, string password)
        {
            return await usersService.TryAsync(x => x.GetUser(userName, password));
        }

        [HttpGet]
        [Route("GetUsers")]
        public User[] GetUsers(bool activeOnly = true)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("SaveUser")]
        public async Task<IAsyncServiceResult> SaveUser(User user)
        {
            return await usersService.TryAsync(x => x.SaveUser(user));
        }

        [HttpGet]
        [Route("PresUser")]
        public PresUser[] SearchUsers(int pageIndex, int pageSize, string sortKey, string sortDir, out int totalResultCount)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("ValidateUser")]
        public bool ValidateUser(User user, out string errorMsg)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("VerifyLogin")]
        public bool VerifyLogin(int userID, string password)
        {
            throw new NotImplementedException();
        }
    }
}
