using MOP.Abstract.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MOP.Abstract
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationDto>> GetAllReservationsAsync();
        Task<IEnumerable<ReservationDto>> GetReservationsForUserAsync(Guid userId);
        Task<IEnumerable<ReservationDto>> GetByConferenceRoomId(Guid conferenceRoomId);
        Task<ReservationDto> AddReservationAsync(ReservationDto dto);
        Task<ReservationDto> UpdateReservationAsync(ReservationDto dto);
        Task<ReservationDto> DeleteReservationAsync(ReservationDto dto);
        Task<ReservationDto> GetById(Guid id);
    }
}
