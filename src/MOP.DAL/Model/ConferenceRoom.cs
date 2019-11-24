using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOP.DAL.Model
{
    public class ConferenceRoom
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public int Size { get; set; }

        public IList<Reservation> Reservations { get; set; }
    }
}
