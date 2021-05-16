using Finance.Domain.Interfaces;
using Finance.Domain.Models;
using Finance.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Finance.Infrastructure.Data.Repositories
{
    public class PhotoRepository
    {
        private readonly DbSet<Photo> _dbSet;
        private readonly IPhotoRepository _photoRepository;

        public PhotoRepository(FinanceDBContext dbContext,IPhotoRepository photoRepository)
        {
            _photoRepository = photoRepository;
            _dbSet = dbContext.Set<Photo>();
        }
    }
}