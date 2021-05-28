using System.Threading.Tasks;
using Finance.Domain.Models;
using Finance.Infrastructure.Data.Context;
using Finance.Infrastructure.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Finance.Infrastructure.Data.Repositories
{
    public class AppUserRepository : AppUser, IAppUserRepository
    {
        private readonly DbSet<AppUser> _dbSet;
        private readonly IUserAccesor _userAccessor;

        public AppUserRepository(FinanceDBContext dbContext, IUserAccesor userAccessor)
        {
            _userAccessor = userAccessor;
            _dbSet = dbContext.Set<AppUser>();
        }

        public async Task<AppUser> GetUser()
        {
            return await _dbSet
                .FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());
        }

        public async Task<AppUser> GetUserPhoto()
        {
            return await _dbSet
                .Include(p => p.Photos)
                .FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());
        }

        public async Task<AppUser> GetUserByUrlUsername(string userName)
        {
            return await _dbSet
                .Include(d => d.UserDescription)
                .Include(p => p.Photos)
                .FirstOrDefaultAsync(x => x.UserName == userName);
        }
    }
}