using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using DataConnectionEntity;
using RESTContact.Models;
using DataConnectionEntity.Entities;
using System.Net.Http.Formatting;
using System.Data.Entity.Validation;
using CommonFunctions;

namespace RESTContact.Controllers
{
    public class ContactsController : BaseAPIController
    {

        private object SaveObjectReturn;
        private object objResponse;
        public ContactsController(IDataModelRepository repo)
            : base(repo)
        {
        }
        private const int PAGE_SIZE = 50;
        public object Get(int page = 0)
        {
            //Header Auth Starts
            var commonfunc = new Common();
            if (commonfunc.ValidateRequestHeader(Request) == false)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            //Header Auth Ends

            var query = TheRepository.GetAllContacts();
            var baseQuery = query.OrderBy(contact => contact.Id);
            var totalCount = baseQuery.Count();
            var totalPages = Math.Ceiling((double)totalCount / PAGE_SIZE);

            var helper = new UrlHelper(Request);
            var links = new List<LinkModel>();
            if (page > 0)
            {
                links.Add(TheModelFactory.CreateLink(helper.Link("Contacts", new { page = page - 1 }), "prevPage", "prev", "next"));

            }

            if (page < totalCount - 1)
            {
                links.Add(TheModelFactory.CreateLink(helper.Link("Contacts", new { page = page + 1 }), "nextPage", "prev", "next"));
            }

            var results = baseQuery
                .ToList()
                .Select(contact => TheModelFactory.Create(contact));
            return new
            {
                TotalCount = totalCount,
                TotalPage = totalPages,
                Links = links,
                Contacts = results
            };
        }


        //api/contact/1
        public object Get(string id, int page = 0)
        {

            //Header Auth Starts
            var commonfunc = new Common();
            if (commonfunc.ValidateRequestHeader(Request) == false)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            //Header Auth Ends

            var contactidexist = TheRepository.GetContact(id);
            if (contactidexist != null)
            {
                return TheModelFactory.Create(TheRepository.GetContact(id));
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Contact Id Does Not Exists");
            }
        }


        public object Post([FromBody] ContactModel model)
        {
            string targetlink;
            string newID;
            var response = new HttpResponseMessage();
            //Header Auth Starts
            var commonfunc = new Common();
            if (commonfunc.ValidateRequestHeader(Request) == false)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            //Header Auth Ends

            try
            {


                var contactentity = TheModelFactory.ParseContact(model);


                if (contactentity == null)
                {
                    objResponse = "Could not read required parameters in body";
                    response = Request.CreateResponse(HttpStatusCode.OK, objResponse);
                    return response;
                }


                // Save the new Entry
                SaveObjectReturn = TheRepository.InsertContact(contactentity);

                if (SaveObjectReturn.ToString() == "true")
                {
                    response = Request.CreateResponse(HttpStatusCode.Created);
                    newID = contactentity.Id;
                    targetlink = $"~/api/contacts/{newID}";

                    return Request.CreateResponse(HttpStatusCode.Created, TheModelFactory.Create(contactentity));
                }
                else
                {
                    objResponse = SaveObjectReturn.ToString();
                    response = Request.CreateResponse(HttpStatusCode.OK, objResponse);
                    return response;
                }
                //return response;


                return Request.CreateResponse(HttpStatusCode.Created, TheModelFactory.Create(contactentity));
                //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not save to the database.");


            }

            catch (DbEntityValidationException dbEx)
            {
                return dbEx.InnerException;
            }
        }


        public object Put([FromBody] ContactModel model)
        {
            string targetlink;
            string newID;
            var response = new HttpResponseMessage();
            //Header Auth Starts
            var commonfunc = new Common();
            if (commonfunc.ValidateRequestHeader(Request) == false)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            //Header Auth Ends

            try
            {


                var contactentity = TheModelFactory.ParseContact(model);

                if (contactentity == null)
                {
                    objResponse = "Could not read required parameters in body";
                    response = Request.CreateResponse(HttpStatusCode.OK, objResponse);
                    return response;
                }

                var results = TheRepository.GetContacts(model.Id)
                                .Where(s => s.Id == model.Id)
                                .ToList();

                if (results.Count != 0)
                {
                    // Save the new Entry
                    try
                    {
                        SaveObjectReturn = TheRepository.UpdateContact(contactentity);
                    }
                    catch
                    {
                        return null;
                    };
                }
                else
                {
                    objResponse = "Id Not Found for Updating" + model.Id;
                    response = Request.CreateResponse(HttpStatusCode.OK, objResponse);
                    return response;
                }


                if (SaveObjectReturn.ToString() == "true")
                {
                    response = Request.CreateResponse(HttpStatusCode.Created);
                    newID = contactentity.Id;
                    targetlink = $"~/api/contacts/{newID}";
                    return Request.CreateResponse(HttpStatusCode.Created, TheModelFactory.Create(contactentity));
                }
                else
                {
                    objResponse = "Failed In Updating Id" + model.Id;
                    response = Request.CreateResponse(HttpStatusCode.OK, objResponse);
                }


                return Request.CreateResponse(HttpStatusCode.Created, TheModelFactory.Create(contactentity));
                //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not save to the database.");


            }

            catch (DbEntityValidationException dbEx)
            {
                return dbEx.InnerException;
            }
        }


        public HttpResponseMessage Delete(string id)
        {
            //Header Auth Starts
            var commonfunc = new Common();
            if (commonfunc.ValidateRequestHeader(Request) == false)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            //Header Auth Ends

            var response = new HttpResponseMessage();
            string targetlink;

            var results = TheRepository.GetContacts(id)
                                .Where(s => s.Id == id)
                                .ToList();

            if (results.Count != 0)
            {
                if (TheRepository.DeleteContact(id))
                {
                    objResponse = "Success:Deleted Id" + id;
                    response = Request.CreateResponse(HttpStatusCode.OK, objResponse);
                }
                else
                {
                    objResponse = "Error:Failed In Deletion Id" + id;
                    response = Request.CreateResponse(HttpStatusCode.OK, objResponse);
                }
            }
            else
            {
                objResponse = "Error:Provided Id Does Not Exists:" + id;
                response = Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }

            targetlink = $"~/api/contacts/{id}";
            return response;

        }

    }
}
