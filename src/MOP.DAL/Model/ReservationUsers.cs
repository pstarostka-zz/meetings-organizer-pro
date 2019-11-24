using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOP.DAL.Model
{
    public class ReservationUser
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ReservationId { get; set; }

        // Relationships

        public User User { get; set; }
        public Reservation Reservation { get; set; }
    }
}
