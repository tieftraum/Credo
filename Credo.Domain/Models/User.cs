using System;
using System.Collections.Generic;
using System.Text;

namespace Credo.Domain.Models
{
    public class User : BaseObject
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string PersonalNumber { get; set; }
        public string Password { get; set; }
        public DateTime DoB { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }
}
