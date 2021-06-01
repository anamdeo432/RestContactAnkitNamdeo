using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Routing;
using DataConnectionEntity;
using DataConnectionEntity.Entities;
using System.Net.Http;


namespace RESTContact.Models
{
    public class ModelFactory
    {
        private UrlHelper _urlHelper;
        private IDataModelRepository _repo;
        private Contact contactobj;


        public ModelFactory(HttpRequestMessage request, IDataModelRepository repo)
        {
            _urlHelper = new UrlHelper(request);
            _repo = repo;
        }

        public ContactModel Create(Contact acc)
        {

            return new ContactModel()
            {
                Id = acc.Id,
                FirstName = acc.FirstName,
                LastName = acc.LastName,
                Organization = acc.Organization,  
                Email = acc.Email,
                PhoneNumber = acc.PhoneNumber,
                Address = acc.Address,
                Status = acc.Status,
                _links = new List<LinkModel>
                {
                    CreateLink(_urlHelper.Link("Contacts", new { acc.Id }), "self", "prev", "next"),
                },
            };
        }


        public LinkModel CreateLink(string href, string rel, string prev, string next, bool isTemplated = false, string method = "GET")
        {
            return new LinkModel
            {
                Href = href,
                Rel = rel,
                Prev = prev,
                Next = next,
                Method = method,
            };
        }


        public Contact ParseContact(ContactModel model)
        {
            try
            {

                if (model == null)
                {
                    return null;
                }
                else if (model.Id == null || model.FirstName == null || model.LastName == null || model.Email == null || model.Organization == null || model.Address == null || model.Status == null)
                {
                    return null;
                }
                contactobj = new Contact();
                contactobj.Id = model.Id;
                contactobj.FirstName = model.FirstName;
                contactobj.LastName = model.LastName;
                contactobj.Organization = model.Organization;
                contactobj.Email = model.Email;
                contactobj.PhoneNumber = model.PhoneNumber;
                contactobj.Address = model.Address;
                contactobj.Status = model.Status; 
                
                return contactobj;
            }

            catch (Exception ex)
            {
                var exc = ex.InnerException.ToString();
                return null;
            }
        }

    }
}