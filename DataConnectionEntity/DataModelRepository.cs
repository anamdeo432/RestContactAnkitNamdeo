using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataConnectionEntity.Entities;


namespace DataConnectionEntity
{
    public class DataModelRepository : IDataModelRepository
    {
        private DataModelContext _ctx;
        
        public DataModelRepository(DataModelContext ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<Contact> GetAllContacts()
        {
            return _ctx.Contacts;
        }

        public Contact GetContact(string id)
        {
            return _ctx.Contacts.Where(f => f.Id == id).FirstOrDefault(); 
        }

        public object InsertContact(Contact contact)
        {
            try
            {
                _ctx.Contacts.Add(contact);
                _ctx.SaveChanges();

                return "true";
            }
            catch (Exception ex)
            {
                return ex.InnerException; 
            }
        }


        public object UpdateContact(Contact contact)
        {
            try
            {
                var result = _ctx.Contacts.SingleOrDefault(b => b.Id == contact.Id );
                if (result != null)
                {
                    result.FirstName = contact.FirstName;
                    result.LastName = contact.LastName;
                    result.Organization = contact.Organization;
                    result.Email = contact.Email;
                    result.PhoneNumber = contact.PhoneNumber;
                    result.Address = contact.Address;
                    result.Status = contact.Status;
                    _ctx.SaveChanges();
                }
                return "true";

            }
            catch (Exception)
            {
                return "false";
            }
        }


        public bool DeleteContact(string id)
        {
            try
            {
                var entity = _ctx.Contacts.Where(d => d.Id == id).FirstOrDefault();
                if (entity != null)
                {
                    _ctx.Contacts.Remove(entity);
                    _ctx.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                var geterror = ex.Message;
                return false;
            }

            return false;
        }

        public IQueryable<Contact> GetContacts(string id)
        {
            return _ctx.Contacts.Where(d => d.Id == id);
        }

        }
}
