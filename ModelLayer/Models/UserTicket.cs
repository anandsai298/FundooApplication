using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.Models
{
    public class UserTicket
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime IssueDateTime { get; set; }
    }
}
