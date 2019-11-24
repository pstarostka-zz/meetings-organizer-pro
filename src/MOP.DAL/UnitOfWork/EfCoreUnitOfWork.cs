using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MOP.DAL.Model;
using MOP.DAL.Repositories;
using MOP.DAL.Repositories.Interfaces;

namespace MOP.DAL.UnitOfWork
{
    public class EfCoreUnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly EfCoreContext _context;

        public EfCoreUnitOfWork(EfCoreContext context)
        {
            _context = context;
        }


        private IUserRepository _userRepo;
        public IUserRepository UserRepo => _userRepo ??= new UserRepository(_context);

        private IConferenceRoomRepository _conferenceRoomRepo;
        public IConferenceRoomRepository ConferenceRoomRepo => _conferenceRoomRepo ??= new ConferenceRoomRepository(_context);

        private IReservationRepository _reservationRepo;
        public IReservationRepository ReservationRepo => _reservationRepo ??= new ReservationRepository(_context);

        public void Dispose()
        {
            using var connection = _context.Database.GetDbConnection();
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
            }
        }
    }
}
