using Finance.Domain.Interfaces.Base;
using Finance.Domain.Models.Base;
using Finance.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<IReadOnlyList<TEntity>> GetAll()
        {
            //return _context.Set<T>().AsEnumerable().OrderByDescending(c => c.Id);
            //foreach (var firstItem in _context.Set<TransactionType>().Include(x=>x.Transactions))
            //{
            //    foreach (var item in firstItem.Transactions)
            //    {
            //        Console.WriteLine("1 ");
            //    }

            //}

            //     IEnumerable<TransactionRepository> outer = _context.Set<TransactionRepository>();
            //   IEnumerable<TransactionType> inner = _context.Set<TransactionType>();

            //var transactionInner =
            //from transaction in outer
            //join transactionType in inner
            //on transaction.TransactionType equals transactionType.Id
            //select new { transaction = transaction.TransactionType, transactionType = transactionType.TransactionTypes };

            //var crossJoin =
            //from transaction in outer
            //from transactionType in inner
            //select new { transaction = transaction.TransactionTypes, transactionType = transactionType.TransactionTypes };

            //var leftJoin =
            //from transaction in outer
            //join transactionType in inner
            //on transaction.TransactionType equals transactionType.Id into subcategories
            //from subcategory in subcategories.DefaultIfEmpty()
            //select new { transaction = transaction.TransactionTypes, transactionType = subcategory.TransactionTypes };


            return await _context.Set<TEntity>().ToListAsync();
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

        public async Task Put(TEntity editModel)
        { 
            TEntity transaction = await _context.Set<TEntity>().FirstOrDefaultAsync(entity => entity.Id == editModel.Id);
             _context.Entry(transaction).State = EntityState.Modified;                  
        }


        public async Task Delete(Guid id)
        {
            TEntity transaction = await _context.Set<TEntity>().FirstOrDefaultAsync(entity => entity.Id == id);
            if (transaction != null)
            {
                _context.Set<TEntity>().Remove(transaction);
            }           
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync()>0;
        }
    }
}
