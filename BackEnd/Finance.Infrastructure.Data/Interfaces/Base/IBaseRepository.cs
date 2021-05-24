using System;
using System.Linq;
using System.Threading.Tasks;
using Finance.Domain.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace Finance.Infrastructure.Data.Interfaces.Base
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetById(Guid id);
        Task<IQueryable<TEntity>> GetAll();
        Task<TEntity> Post(TEntity newIncome);
        Task<TEntity> Put(TEntity editIncome);
        Task Delete(Guid id);
        Task<bool> SaveChanges();
    }
}