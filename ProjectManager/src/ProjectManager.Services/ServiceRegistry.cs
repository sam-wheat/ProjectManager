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
            string epcName = EndPointCollectionName.ProjectManager.ToString();
            EndPointType endPointType = EndPointType.InProcess;

            registrationHelper.RegisterService<UsersService, IUsersService>(endPointType, epcName);
            registrationHelper.RegisterService<ProjectsService, IProjectsService>(endPointType, epcName);
            registrationHelper.RegisterService<ContactsService, IContactsService>(endPointType, epcName);
            registrationHelper.RegisterService<ActivitiesService, IActivitiesService>(endPointType, epcName);
            registrationHelper.RegisterService<DatabaseUtilitiesService, IDatabaseUtilitiesService>(endPointType, epcName);
            registrationHelper.RegisterService<DefaultContactsService, IDefaultContactsService>(endPointType, epcName);
            registrationHelper.RegisterService<RemindersService, IRemindersService>(endPointType, epcName);
        }
    }
}
