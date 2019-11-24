using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOP.DAL.Model
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Password{ get; set; }
        public string PhoneNo { get; set; }


        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<ReservationUser> ReservationUsers { get; set; }

    }
}
