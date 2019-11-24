using System;
using System.Collections.Generic;
using System.Text;

namespace MOP.Abstract.Dto
{
    public class ReservationDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ConferenceRoomId { get; set; }
        public DateTime ReservationStart { get; set; }
        public DateTime ReservationEnd { get; set; }
        public IEnumerable<UserDto> InvitedUsers { get; set; }
    }
}
