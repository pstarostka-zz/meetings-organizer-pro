using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOP.DAL.Repositories.Interfaces
{
    public interface IGetRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Guid id);
    }
}
