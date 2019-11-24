using System;
using System.Collections.Generic;
using System.Text;

namespace MOP.Abstract.Dto
{
    public class ReservationUsersDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ReservationId { get; set; }

    }
}
