using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataConnectionEntity.Entities
{
    public class Contact
    {
        public string Id { get; set; }

        //[Key, Column(Order = 1)]
        public string FirstName { get; set; }

        //[Key, Column(Order = 2)]
        public string LastName { get; set; }

        //[Key, Column(Order = 3)]
        public string Organization { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Status { get; set; }

    }
}
