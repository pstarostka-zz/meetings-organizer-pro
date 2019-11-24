
using Dapper;
using Microsoft.EntityFrameworkCore;
using MOP.DAL.Model;
using MOP.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOP.DAL.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {

        public UserRepository(EfCoreContext context) : base(context)
        {
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await Context.Users.FirstOrDefaultAsync(x => x.Email == email).ConfigureAwait(false);
        }

        public async Task<IEnumerable<User>> GetUsersByReservationId(Guid reservationId)
        {
            return await Context.Users.Include(x => x.ReservationUsers)
                                          .Where(x => x.ReservationUsers
                                                  .Where(y => y.ReservationId == reservationId)
                                                  .Select(y => y.Id)
                                                  .Contains(x.Id)
                                          ).ToListAsync().ConfigureAwait(false);
        }
    }
}
