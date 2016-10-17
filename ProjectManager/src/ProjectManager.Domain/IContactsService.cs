using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ServiceModel;
using ProjectManager.Model;

namespace ProjectManager.Domain
{
    public interface IContactsService
    {
        [OperationContract]
        Contact GetContact(int id);
        [OperationContract]
        bool SaveContact(Contact contact, out string errorMsg);
        [OperationContract]
        int DeleteContact(int contactID);
        [OperationContract]
        Contact[] GetAvailableContactsForProject(int projectID, int userID);
        [OperationContract]
        PresContact[] SearchContacts(int userID, int PageIndex, int PageSize, string SortKey, string SortDir, string SearchContactName, string SearchCompanyName, out int totalResultCount);
        [OperationContract]
        Contact[] GetContactsForUser(int userID);
    }
}
