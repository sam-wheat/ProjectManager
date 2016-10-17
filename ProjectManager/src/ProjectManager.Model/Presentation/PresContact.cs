using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Model;
using ProjectManager.Model.Domain;

namespace ProjectManager.Model.Presentation
{
    public class PresContact : BasePresentationModel
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public PresContact()
        { 

        }

        public PresContact(Contact contact)
        {
            ID = contact.ID;
            UserID = contact.UserID;
            Name = contact.Name;
            Company = contact.Company;
            Email = contact.Email;
            Phone = contact.Phone;
        }

        public Contact ToContact()
        {
            return new Contact
            {
                ID = this.ID,
                UserID = this.UserID,
                Name = this.Name,
                Company = this.Company,
                Email = this.Email,
                Phone = this.Phone
            };
        }
    }
}
