using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectManager.Core;
using ProjectManager.Domain;

namespace ProjectManager.Services.REST
{
    public class ServiceRegistry
    {
        private IRegistrationHelper registrationHelper;

        public ServiceRegistry(IRegistrationHelper registrationHelper)
        {
            this.registrationHelper = registrationHelper;
        }

        public void Register()
        {
            string apiName = APIName.ProjectManager.ToString();
            EndPointType endPointType = EndPointType.REST;
            registrationHelper.RegisterService<UsersService, IUsersService>(endPointType, apiName);
        }
    }
}
