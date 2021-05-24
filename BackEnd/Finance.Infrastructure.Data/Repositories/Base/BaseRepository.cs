using System;
using System.Linq;
using System.Threading.Tasks;
using Finance.Domain.Models.Base;
using Finance.Infrastructure.Data.Context;
using Finance.Infrastructure.Data.Interfaces.Base;
using Microsoft.EntityFrameworkCore;

namespace Finance.Infrastructure.Data.Repositories.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly FinanceDBContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(FinanceDBContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<IQueryable<TEntity>> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<TEntity> GetById(Guid id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public async Task<TEntity> Post(TEntity newModel)
        {
            await _dbSet.AddAsync(newModel);
            return newModel;
        }

        public async Task<TEntity> Put(TEntity editModel)
        {
            _context.Entry(editModel).State = EntityState.Modified;
            return editModel;
        }


        public async Task Delete(Guid id)
        {
            var model = await _dbSet.FirstOrDefaultAsync(entity => entity.Id == id);
            if (model != null) _dbSet.Remove(model);
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}