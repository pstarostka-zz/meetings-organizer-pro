using Microsoft.EntityFrameworkCore;
using MOP.DAL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace MOP.DAL
{
    public class EfCoreContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ConferenceRoom> ConferenceRooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationUser> ReservationUsers { get; set; }

        public EfCoreContext(DbContextOptions<EfCoreContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            if (modelBuilder == null)
                throw new ArgumentNullException(nameof(modelBuilder), "Value cannot be null");


            modelBuilder.Entity<User>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Reservation>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<ConferenceRoom>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<ReservationUser>().Property(x => x.Id).HasDefaultValueSql("NEWID()");

            //modelBuilder.Entity<Reservation>()
            //    .HasKey(x => new { x.UserId, x.ConferenceRoomId, });

            modelBuilder.Entity<Reservation>()
           .HasOne(pt => pt.User)
           .WithMany(p => p.Reservations)
           .HasForeignKey(pt => pt.UserId);

            modelBuilder.Entity<Reservation>()
                .HasOne(pt => pt.ConferenceRoom)
                .WithMany(t => t.Reservations)
                .HasForeignKey(pt => pt.ConferenceRoomId);

            modelBuilder.Entity<ReservationUser>()
                 .HasKey(x => new { x.UserId, x.ReservationId });

            modelBuilder.Entity<ReservationUser>()
              .HasOne(pt => pt.User)
              .WithMany(u => u.ReservationUsers)
              .HasForeignKey(pt => pt.UserId)
              .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ReservationUser>()
                .HasOne(pt => pt.Reservation)
                .WithMany(r => r.ReservationUsers)
                .HasForeignKey(pt => pt.ReservationId)
                .OnDelete(DeleteBehavior.NoAction);


        }
        public override void Dispose()
        {
            var _connection = this.Database.GetDbConnection();

           
            //base.Dispose();
        }
    }
}
