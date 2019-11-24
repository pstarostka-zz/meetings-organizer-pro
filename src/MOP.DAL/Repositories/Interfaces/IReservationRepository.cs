using MOP.DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MOP.DAL.Repositories.Interfaces
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        Task<IEnumerable<Reservation>> GetByUserId(Guid userId);
        Task<IEnumerable<Reservation>> GetByConferenceRoomId(Guid conferenceRoomId);
        Task<int> InsertReservationWithUsers(Reservation reservation, IEnumerable<User> users);
        Task<int> RemoveUserFromReservation(Guid reservationId, Guid userId);
        Task<int> AddUserToReservation(Guid reservationId, Guid userId);
        Task<int> Update(Reservation reservation, IEnumerable<User> users);
    }
}
