﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Model;
using ProjectManager.BusinessLogic.Presentation;

namespace ProjectManager.BusinessLogic.Services
{
    public partial class DataServices
    {
        private void attachContact(Contact contact)
        {
            if (contact.ID == 0)
                db.Contacts.Add(contact);
            else
                db.AttachAsModfied(contact);
        }

        private void deleteContact(Contact contact)
        {
            db.AttachAsDeleted(contact);
        }

        public Contact GetContact(int id)
        {
            return db.Contacts.SingleOrDefault(x => x.ID == id);
        }

        public bool SaveContact(Contact contact, out string errorMsg)
        {
            bool result = false;
            errorMsg = "";
            result = ValidateContact(contact, out errorMsg);

            if (result)
            {
                attachContact(contact);
                result = db.SaveChanges() > 0;
            }
            return result;
        }

        public int DeleteContact(int contactID)
        {
            deleteDefaultContacts(db.DefaultContacts.Where(x => x.ContactID == contactID));
            deleteContact(db.Contacts.SingleOrDefault(x => x.ID == contactID));
            return db.SaveChanges();
        }

        public PresContact[] SearchContacts(int userID, int PageIndex, int PageSize, string SortKey, string SortDir, string contactName, string companyName, out int totalResultCount)
        {
            if (string.IsNullOrEmpty(contactName))
                contactName = null;
            if (string.IsNullOrEmpty(companyName))
                companyName = null;

            IQueryable<Contact> query = db.Contacts
                .Where(x => x.UserID == userID && (x.Name.Contains(contactName) || contactName == null) && (x.Company.Contains(companyName) || companyName == null));
             
            totalResultCount = query.Count();
            
            if(SortDir == "ASC")
            {
                if (SortKey == "companyName")
                    query = query.OrderBy(x => x.Company);
                else
                    query = query.OrderBy(x => x.Name);
            }
            else if(SortDir == "DESC")
            {
                if (SortKey == "companyName")
                    query = query.OrderByDescending(x => x.Company);
                else
                    query = query.OrderByDescending(x => x.Name);
            }
            else 
            {
                throw new Exception("SortDir is invalid: " + SortDir);
            }

            return query
                .Skip((PageIndex -1) * PageSize)
                .Take(PageSize)
                .ToList()
                .Select(x => new PresContact(x)).ToArray();
        }

        public Contact[] GetContactsForUser(int userID)
        {
            return db.Contacts.Where(x => x.UserID == userID).OrderBy(x => x.Name).ToArray();
        }

        public Contact[] GetAvailableContactsForProject(int projectID, int userID)
        {
            var list = from c in db.Contacts
                       where c.UserID == userID
                       && ! c.DefaultContacts.Any(x => x.ProjectID == projectID)
                       select c;
            
            return list.ToArray();
        }

        public PresDefaultContact[] GetPresDefaultContactsForProject(int projectID)
        {
            var list = db.DefaultContacts
                .Include("Contact")
                .Include("Project")
                .Where(x => x.ProjectID == projectID).ToList();

            return list.Select(x => new PresDefaultContact(x))
                .OrderBy(x => x.ContactName).ToArray() ?? new PresDefaultContact[0];
        }
        
        public bool ValidateContact(Contact contact, out string errorMsg)
        {
            errorMsg = "";
            
            if (String.IsNullOrEmpty(contact.Name)) 
            {
                errorMsg = "Contact name cannnot be blank.";
                return false;
            }
            return true;
        }
    }
}
