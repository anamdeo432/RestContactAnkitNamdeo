using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; //used for Maximum StringLength Attribute
using System.ComponentModel.DataAnnotations.Schema;


namespace RESTContact.Models
{
    public class ContactModel
    {
        public string Id { get; set; }
        
        public string FirstName { get; set; }

        
        public string LastName { get; set; }

        
        public string Organization { get; set; }

        
        public string Email { get; set; }

        
        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Status { get; set; }

        public IEnumerable<LinkModel> _links { get; set; }
    }
}