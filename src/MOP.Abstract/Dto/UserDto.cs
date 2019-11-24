using System;
using System.Collections.Generic;
using System.Text;

namespace MOP.Abstract.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
    }
}
