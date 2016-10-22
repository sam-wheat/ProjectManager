using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ServiceModel;
using ProjectManager.Model.Domain;
using ProjectManager.Model.Presentation;

namespace ProjectManager.Domain
{
    public interface IDefaultContactsService : IDisposable
    {
        [OperationContract]
        void AttachDefaultContact(DefaultContact defContact);
        [OperationContract]
        void DeleteDefaultContact(DefaultContact defContact);
        [OperationContract]
        DefaultContact[] GetDefaultContactsForProject(int projectID);
        [OperationContract]
        PresDefaultContact[] GetPresDefaultContactsForProject(int projectID);
        [OperationContract]
        int ModifyDefaultContact(DefaultContact defContact);
        [OperationContract]
        int DeleteDefaultContactAndSave(DefaultContact defContact);
        [OperationContract]
        void DeleteDefaultContacts(IEnumerable<DefaultContact> defContacts);
        
    }
}
