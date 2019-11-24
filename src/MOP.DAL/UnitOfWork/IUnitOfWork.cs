using MOP.DAL.Model;
using MOP.DAL.Repositories.Interfaces;

namespace MOP.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IUserRepository UserRepo { get; }
        public IConferenceRoomRepository ConferenceRoomRepo { get; }
        public IReservationRepository ReservationRepo { get; }
    }
}
