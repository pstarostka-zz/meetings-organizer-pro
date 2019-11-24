namespace MOP.DAL.Repositories.Interfaces
{
    public interface IRepository<T> : IGetRepository<T>, IWriteRepository<T> where T : class
    {
    }
}
