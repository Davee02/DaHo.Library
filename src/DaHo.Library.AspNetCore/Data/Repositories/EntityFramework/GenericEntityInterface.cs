using System.Collections.Generic;
using System.Threading.Tasks;
using DaHo.Library.AspNetCore.Data.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DaHo.Library.AspNetCore.Data.Repositories.EntityFramework
{
    public abstract class GenericEntityInterface<TEntity> : IGenericInterface<TEntity> where TEntity : class
    {
        private readonly DbContext _context;

        protected GenericEntityInterface(DbContext context)
        {
            _context = context;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.FindAsync<TEntity>(id);
        }

        public virtual async Task CreateAsync(TEntity entity)
        {
            await _context.Set<TEntity>()
                .AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>()
                .Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>()
                .Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
