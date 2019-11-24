using MOP.Abstract.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MOP.Abstract
{
    public interface IConferenceRoomService
    {
        Task<IEnumerable<ConferenceRoomDto>> GetAllConferenceRoomsAsync();
        Task<ConferenceRoomDto> GetConferenceRoomByIdAsync(Guid id);
        Task<bool> DeleteConferenceRoomAsync(ConferenceRoomDto dto);
        Task<ConferenceRoomDto> AddConferenceRoomAsync(ConferenceRoomDto dto);
    }
}
