using System.Threading.Tasks;

namespace MOP.DAL.Repositories.Interfaces
{
    public interface IWriteRepository<T> where T : class
    {
        Task<int> Add(T entity);
        Task<int> Update(T entity);
        Task<int> Delete(T entity);
    }
}
