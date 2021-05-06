using Finance.Domain.Models;
using Finance.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finance.Domain.Interfaces.Base
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetById(Guid id);
        Task<IReadOnlyList<TEntity>> GetAll();
        Task<TEntity> Post(TEntity newIncome);
        Task Put(TEntity editIncome);
        Task Delete(Guid id);
        Task<bool> SaveChanges();
    }
}