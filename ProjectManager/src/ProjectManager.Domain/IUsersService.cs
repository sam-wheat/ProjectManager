using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ServiceModel;
using ProjectManager.Core;
using ProjectManager.Model;
using ProjectManager.Model.Domain;
using ProjectManager.Model.Presentation;

namespace ProjectManager.Domain
{
    [ServiceContract]
    public interface IUsersService : IDisposable
    {
        [OperationContract]
        Task<IAsyncServiceResult<User>> GetUser(string userName, string password);
        [OperationContract]
        Task<IAsyncServiceResult> SaveUser(User user);
        [OperationContract]
        bool ValidateUser(User user, out string errorMsg);
        [OperationContract]
        int DeleteUser(User user);
        [OperationContract]
        int DeleteUserByID(int userID);
        [OperationContract]
        User[] GetUsers(bool activeOnly = true);
        [OperationContract]
        PresUser[] SearchUsers(int pageIndex, int pageSize, string sortKey, string sortDir, out int totalResultCount);
        [OperationContract]
        bool VerifyLogin(int userID, string password);
    }
}
