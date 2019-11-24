using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOP.DAL.Model
{
    public class Reservation
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ConferenceRoomId { get; set; }
        public DateTime ReservationStart { get; set; }
        public DateTime ReservationEnd { get; set; }

        //relationships
        public User User { get; set; }
        public ConferenceRoom ConferenceRoom { get; set; }
        public ICollection<ReservationUser> ReservationUsers { get; set; }

    }
}
