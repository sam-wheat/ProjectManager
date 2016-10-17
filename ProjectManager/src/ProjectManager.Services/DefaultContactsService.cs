using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using ProjectManager.Model;

namespace ProjectManager.BusinessLogic.Services
{
    public partial class DataServices
    {
        private void attachDefaultContact(DefaultContact defContact)
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

        public int ModifyDefaultContact(DefaultContact defContact)
        {
            throw new NotImplementedException();
        }

        public int DeleteDefaultContact(DefaultContact defContact)
        {
            deleteDefaultContact(defContact);
            return db.SaveChanges();
        }

        private void deleteDefaultContacts(IEnumerable<DefaultContact> defContacts)
        {
            defContacts.ToList().ForEach(d => deleteDefaultContact(d));
        }

        private void deleteDefaultContact(DefaultContact defContact)
        {
            if (db.Entry(defContact).State == EntityState.Detached)
                db.AttachAsDeleted(defContact);
            else
                db.Entry(defContact).State = EntityState.Deleted;
        }
    }
}
