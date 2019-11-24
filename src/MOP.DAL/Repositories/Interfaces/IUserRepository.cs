using MOP.DAL.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOP.DAL.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByEmail(string email);
        Task<IEnumerable<User>> GetUsersByReservationId(Guid reservationId);
    }
}
