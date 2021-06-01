using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataConnectionEntity.Entities;

namespace DataConnectionEntity
{
    public interface IDataModelRepository
    {
        IQueryable<Contact> GetAllContacts();

        Contact GetContact(string id);

        object InsertContact(Contact contact);

        object UpdateContact(Contact contact);

        bool DeleteContact(string id);

        IQueryable<Contact> GetContacts(string id);
    }
}
