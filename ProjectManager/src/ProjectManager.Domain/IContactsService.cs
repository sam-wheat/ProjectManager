using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ServiceModel;
using ProjectManager.Model.Domain;
using ProjectManager.Model.Presentation;
namespace ProjectManager.Domain
{
    public interface IContactsService : IDisposable
    {
        [OperationContract]
        void DeleteContact(Contact contact);
        [OperationContract]
        Contact GetContact(int id);
        [OperationContract]
        bool SaveContact(Contact contact, out string errorMsg);
        [OperationContract]
        int DeleteContactAndSave(int contactID);
        [OperationContract]
        Contact[] GetAvailableContactsForProject(int projectID, int userID);
        [OperationContract]
        PresContact[] SearchContacts(int userID, int PageIndex, int PageSize, string SortKey, string SortDir, string SearchContactName, string SearchCompanyName, out int totalResultCount);
        [OperationContract]
        Contact[] GetContactsForUser(int userID);
        [OperationContract]
        bool ValidateContact(Contact contact, out string errorMsg);
    }
}
