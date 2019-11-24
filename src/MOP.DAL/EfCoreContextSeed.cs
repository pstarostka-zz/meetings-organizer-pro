using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MOP.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOP.DAL
{
    public static class EfCoreContextSeed
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new EfCoreContext(serviceProvider.GetRequiredService<DbContextOptions<EfCoreContext>>());
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;
            }
            context.Users.AddRange
                (
                    new User
                    {
                        Email = "test@test.com",
                        Password = "test",
                        FirstName = "Test",
                        LastName = "Surname",
                        PhoneNo = "123123123",
                        Title = "Project Manager"
                    }, new User
                    {
                        Email = "test2@test.com",
                        FirstName = "Test",
                        LastName = "Surname",
                        PhoneNo = "123123123",
                        Title = "Project Manager"
                    }, new User
                    {
                        Email = "test3@test.com",
                        FirstName = "Test",
                        LastName = "Surname",
                        PhoneNo = "123123123",
                        Title = "Project Manager"
                    }
                );

            context.ConferenceRooms.AddRange
                (
                    new ConferenceRoom
                    {
                        Name = "Mordor",
                        PhoneNo = "338258282",
                        Size = 10
                    }, new ConferenceRoom
                    {
                        Name = "Shire",
                        PhoneNo = "338258282",
                        Size = 6
                    }, new ConferenceRoom
                    {
                        Name = "Minas Tirith",
                        PhoneNo = "338258282",
                        Size = 15
                    }, new ConferenceRoom
                    {
                        Name = "Rohan",
                        PhoneNo = "338258282",
                        Size = 8
                    }
                );

            context.SaveChangesAsync().GetAwaiter().GetResult();
        }
    }
}
