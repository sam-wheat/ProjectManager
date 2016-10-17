using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ServiceModel;
using ProjectManager.Model;

namespace ProjectManager.Domain
{
    public interface IDefaultContactsService
    {
        [OperationContract]
        DefaultContact[] GetDefaultContactsForProject(int projectID);
        [OperationContract]
        PresDefaultContact[] GetPresDefaultContactsForProject(int projectID);
        [OperationContract]
        int ModifyDefaultContact(DefaultContact defContact);
        [OperationContract]
        int DeleteDefaultContact(DefaultContact defContact);
        [OperationContract]
        bool ValidateContact(Contact contact, out string errorMsg);
    }
}
