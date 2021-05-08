using Finance.Domain.Models;
using Finance.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Finance.Domain.Interfaces.Base
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetById(Guid id);
        Task<IReadOnlyList<TEntity>> GetAll();
        Task<TEntity> Post(TEntity newIncome);
        Task<TEntity> Put(TEntity editIncome);
        Task Delete(Guid id);
        Task<bool> SaveChanges();
        Task<EntityState> DetachObject(TEntity model);
    }
}