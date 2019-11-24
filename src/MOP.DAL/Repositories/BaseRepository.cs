using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MOP.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace MOP.DAL.Repositories
{
    abstract public class BaseRepository<T> : IRepository<T>, IDisposable where T : class
    {
        protected BaseRepository(EfCoreContext context)
        {
            Context = context;
        }

        protected EfCoreContext Context { get; }


        public virtual async Task<int> Add(T entity)
        {
            await Context.Set<T>().AddAsync(entity);
            return await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        public virtual async Task<int> Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
            return await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            using var connection = Context.Database.GetDbConnection();
            return await connection.GetAllAsync<T>().ConfigureAwait(false);
        }

        public virtual async Task<T> GetById(Guid id)
        {
            using var connection = Context.Database.GetDbConnection();
            return await connection.GetAsync<T>(id).ConfigureAwait(false);
        }

        public virtual async Task<int> Update(T entity)
        {
            Context.Set<T>().Update(entity);
            return await Context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
