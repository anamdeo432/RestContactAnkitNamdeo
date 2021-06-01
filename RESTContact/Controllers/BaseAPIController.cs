using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataConnectionEntity;
using RESTContact.Models;


namespace RESTContact.Controllers
{
    public abstract class BaseAPIController : ApiController
    {
        private readonly IDataModelRepository _repo;
        private ModelFactory _modelFactory;

        public BaseAPIController(IDataModelRepository repo)
        {
            _repo = repo;
        }

        protected IDataModelRepository TheRepository
        {
            get { return _repo; }
        }

        protected ModelFactory TheModelFactory
        {
            get
            {
                if (_modelFactory == null)
                    _modelFactory = new ModelFactory(Request, TheRepository);
                return _modelFactory;
            }
        }
    }
}