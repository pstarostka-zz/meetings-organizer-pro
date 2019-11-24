using System;
using System.Collections.Generic;
using System.Text;

namespace MOP.Abstract.Dto
{
    public class ConferenceRoomDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public int Size { get; set; }
    }
}
