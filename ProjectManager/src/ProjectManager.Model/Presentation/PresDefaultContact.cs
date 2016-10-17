using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Model;
using ProjectManager.Model.Domain;

namespace ProjectManager.Model.Presentation
{
    public class PresDefaultContact : BasePresentationModel
    {
        public int ProjectID { get; set; }
        public int ContactID { get; set; }
        public string ProjectName { get; set; }
        public string ContactName { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public PresDefaultContact()
        { 
        
        }

        public PresDefaultContact(DefaultContact defaultContact)
        {
            ProjectID = defaultContact.ProjectID;
            ContactID = defaultContact.ContactID;

            if (defaultContact.Contact != null)
            {
                ContactName = defaultContact.Contact.Name;
                Company = defaultContact.Contact.Company;
                Email = defaultContact.Contact.Email;
                Phone = defaultContact.Contact.Phone;
            }

            if (defaultContact.Project != null)
                ProjectName = defaultContact.Project.Name;

            
        }

        public DefaultContact ToDefaultContact()
        {
            return new DefaultContact
            {
                ContactID = ContactID,
                ProjectID = ProjectID
            };
        }
    }
}
