using Microsoft.EntityFrameworkCore;
using MOP.DAL;
using MOP.DAL.Model;
using MOP.DAL.UnitOfWork;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MOP.Testing
{
    public class DbTests
    {
        private readonly EfCoreUnitOfWork _unitOfWork;

        public DbTests()
        {
            var options = new DbContextOptionsBuilder<EfCoreContext>()
                .UseInMemoryDatabase("MOP.Data.Tests").Options;

            var context = new EfCoreContext(options);
            SeedAsync(context).GetAwaiter().GetResult();
            _unitOfWork = new EfCoreUnitOfWork(context);
        }

        [Fact]
        public async Task ShouldReturnAllUsers()
        {
            //var userControler = new UserController(_authService,)
            var query = await _unitOfWork.UserRepo.GetAll().ConfigureAwait(false);
            Assert.Equal(3, query.Count());

        }

        [Fact]
        public async Task ShouldAuthorizeUser()
        {
            var user = await _unitOfWork.UserRepo.GetUserByEmail("test@test.com").ConfigureAwait(false);
            var expectedPassword = "password";

            Assert.Equal(expectedPassword, user.Password);
        }

        private async Task SeedAsync(EfCoreContext context)
        {
            var users = new[]
            {
                new User
                {
                    Email = "test@test.com",
                    Password="password",
                },
                new User
                {
                    Email = "test2@test.com",
                    Password = "password",
                },
                new User
                {
                    Email = "test3@test.com",
                    Password ="password",
                }
            };

            context.Users.AddRange(users);

            await context.SaveChangesAsync();
        }
    }
}
