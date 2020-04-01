using System.Collections.Generic;
using System.Threading.Tasks;
using DaHo.Library.AspNetCore.Data.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DaHo.Library.AspNetCore.Data.Repositories.EntityFramework
{
    public abstract class GenericEntityRepository<TEntity, TContext> : IGenericRepository<TEntity>
        where TEntity : class 
        where TContext : DbContext
    {
        protected readonly TContext Context;

        protected GenericEntityRepository(TContext context)
        {
            Context = context;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Context
                .Set<TEntity>()
                .AsNoTracking()
                .ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            var foundEntity = await Context.FindAsync<TEntity>(id);

            if (foundEntity != null)
                Context.Entry(foundEntity).State = EntityState.Detached;

            return foundEntity;
        }

        public virtual async Task CreateAsync(TEntity entity)
        {
            await Context.Set<TEntity>()
                .AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            Context.Set<TEntity>()
                .Update(entity);
            await Context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            Context.Set<TEntity>()
                .Remove(entity);
            await Context.SaveChangesAsync();
        }
    }
}
