using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MOP.Abstract;
using MOP.DAL;
using MOP.DAL.UnitOfWork;
using MOP.Services;
using System;
using System.Reflection;

namespace MOP.Resolver
{
    public static class ServiceResolver
    {
        public static void ResolveServices(IServiceCollection services, string dbConnectionString)
        {
            services.AddDbContext<EfCoreContext>(options =>
            {
                options.UseSqlServer(dbConnectionString, sqlServerOptionsAction: actions =>
                  {
                      actions.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);

                      actions.EnableRetryOnFailure(maxRetryCount: 5,
                                                   maxRetryDelay: TimeSpan.FromSeconds(30),
                                                   errorNumbersToAdd: null);
                  });
            });
            EfCoreContextSeed.Initialize(services.BuildServiceProvider());

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IUnitOfWork, EfCoreUnitOfWork>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IConferenceRoomService, ConferenceRoomService>();
        }
    }
}
