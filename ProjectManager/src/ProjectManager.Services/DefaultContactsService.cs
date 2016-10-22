using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Model.Domain;
using ProjectManager.Model.Presentation;
using ProjectManager.Domain;

namespace ProjectManager.Services
{
    public class DefaultContactsService : BaseService, IDefaultContactsService
    {
        public DefaultContactsService(MyDbContextOptions options) : base(options)
        {

        }

        public void AttachDefaultContact(DefaultContact defContact)
        {
            //if (defContact.ProjectID == 0 || defContact.ContactID == 0)
            //    throw new Exception("ProjectID and ContactID must be greater than zero.");

            if (!db.DefaultContacts.Any(x => x.ProjectID == defContact.ProjectID && x.ContactID == defContact.ContactID))
                db.DefaultContacts.Add(defContact);
        }

        public DefaultContact[] GetDefaultContactsForProject(int projectID)
        {
            throw new NotImplementedException();
        }

        public PresDefaultContact[] GetPresDefaultContactsForProject(int projectID)
        {
            throw new NotImplementedException();
        }

        public int ModifyDefaultContact(DefaultContact defContact)
        {
            throw new NotImplementedException();
        }

        public int DeleteDefaultContactAndSave(DefaultContact defContact)
        {
            DeleteDefaultContact(defContact);
            return db.SaveChanges();
        }

        public void DeleteDefaultContacts(IEnumerable<DefaultContact> defContacts)
        {
            defContacts.ToList().ForEach(d => DeleteDefaultContact(d));
        }

        public void DeleteDefaultContact(DefaultContact defContact)
        {
            if (db.Entry(defContact).State == EntityState.Detached)
                db.AttachAsDeleted(defContact);
            else
                db.Entry(defContact).State = EntityState.Deleted;
        }
    }
}
