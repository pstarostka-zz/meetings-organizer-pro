using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.EntityFrameworkCore;
using MOP.DAL.Model;
using MOP.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOP.DAL.Repositories
{
    public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(EfCoreContext context) : base(context)
        { }

        public async Task<int> AddUserToReservation(Guid reservationId, Guid userId)
        {
            Context.Add<ReservationUser>(new ReservationUser { ReservationId = reservationId, UserId = userId });
            return await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<Reservation>> GetByConferenceRoomId(Guid conferenceRoomId)
        {
            return await Context.Reservations.Include(x => x.ReservationUsers)
                                              .ThenInclude(x => x.User)
                                              .Where(x => x.ConferenceRoomId == conferenceRoomId)
                                              .ToListAsync()
                                              .ConfigureAwait(false);

        }

        public async Task<IEnumerable<Reservation>> GetByUserId(Guid userId)
        {
            return await Context.Reservations.Include(x => x.ReservationUsers)
                                             .ThenInclude(x => x.User)
                                             .Where(x => x.ReservationUsers.Select(x => x.UserId).Contains(userId))
                                             .ToListAsync()
                                             .ConfigureAwait(false);
        }

        public async Task<int> InsertReservationWithUsers(Reservation reservation, IEnumerable<User> users)
        {
            using var connection = Context.Database.GetDbConnection();

            Context.Reservations.Add(reservation);
            var completed = await Context.SaveChangesAsync().ConfigureAwait(false);

            var resUsers = users.Select(x => new ReservationUser
            {
                Id = Guid.NewGuid(),
                ReservationId = reservation.Id,
                UserId = x.Id
            });
            Context.ReservationUsers.AddRange(resUsers);
            return await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<int> RemoveUserFromReservation(Guid reservationId, Guid userId)
        {
            Context.Remove<ReservationUser>(new ReservationUser { ReservationId = reservationId, UserId = userId });
            return await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        public override async Task<Reservation> GetById(Guid id)
        {
            return await Context.Reservations.Include(ru => ru.ReservationUsers)
                .ThenInclude(y => y.User)
                .Where(x => x.ReservationUsers.Select(y => y.ReservationId).Contains(id))
                .FirstOrDefaultAsync(x => x.Id == id)
                .ConfigureAwait(false);
        }

        public override async Task<IEnumerable<Reservation>> GetAll()
        {
            return await Context.Reservations.Include(ru => ru.ReservationUsers)
              .ThenInclude(y => y.User)
              .ToListAsync()
              .ConfigureAwait(false);
        }

        public override async Task<int> Update(Reservation entity)
        {
            return await base.Update(entity).ConfigureAwait(false);
        }

        public async Task<int> Update(Reservation entity, IEnumerable<User> users)
        {
            if (entity == null)
            {
                return -1;
            }

            if (users != null && users.Any())
            {
                var dbEntity = await Context.Reservations.Include(x => x.ReservationUsers).FirstOrDefaultAsync(x => x.Id == entity.Id).ConfigureAwait(false);
                dbEntity.UserId = entity.UserId;
                dbEntity.ReservationEnd = entity.ReservationEnd;
                dbEntity.ReservationStart = entity.ReservationStart;
                dbEntity.ConferenceRoomId = entity.ConferenceRoomId;

                var userIds = users.Select(x => x.Id);
                Context.Reservations.Update(dbEntity);

                foreach (var reservationUser in dbEntity.ReservationUsers)
                {
                    if (!userIds.Contains(reservationUser.UserId))
                    {
                        Context.ReservationUsers.Remove(reservationUser);
                    }
                }
                var dbUserIds = dbEntity.ReservationUsers.Select(x => x.UserId);
                foreach (var id in userIds)
                {
                    if (!dbUserIds.Contains(id))
                        Context.ReservationUsers.Add(new ReservationUser { ReservationId = dbEntity.Id, UserId = id });
                }

                return await Context.SaveChangesAsync().ConfigureAwait(false);
            }
            else
            {
                return await base.Update(entity).ConfigureAwait(false);
            }
        }

    }
}
