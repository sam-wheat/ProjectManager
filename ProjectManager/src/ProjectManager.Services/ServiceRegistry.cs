using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectManager.Core;
using ProjectManager.Domain;

namespace ProjectManager.Services
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
            EndPointType endPointType = EndPointType.InProcess;

            registrationHelper.RegisterService<UsersService, IUsersService>(endPointType, apiName);
            registrationHelper.RegisterService<ProjectsService, IProjectsService>(endPointType, apiName);
            registrationHelper.RegisterService<ContactsService, IContactsService>(endPointType, apiName);
            registrationHelper.RegisterService<ActivitiesService, IActivitiesService>(endPointType, apiName);
            registrationHelper.RegisterService<DatabaseUtilitiesService, IDatabaseUtilitiesService>(endPointType, apiName);
            registrationHelper.RegisterService<DefaultContactsService, IDefaultContactsService>(endPointType, apiName);
            registrationHelper.RegisterService<RemindersService, IRemindersService>(endPointType, apiName);
        }
    }
}
