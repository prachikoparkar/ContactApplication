using System;
using System.Collections.Generic;

#nullable disable

namespace Webapi.Contact.Models
{
    public partial class Contact
    {
        public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public bool Status { get; set; }
    }
}
