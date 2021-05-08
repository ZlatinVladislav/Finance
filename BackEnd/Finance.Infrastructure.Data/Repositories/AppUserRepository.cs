using System.Threading.Tasks;
using Finance.Domain.Interfaces;
using Finance.Domain.Models;
using Finance.Infrastructure.Data.Context;
using Finance.Infrastructure.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Finance.Infrastructure.Data.Repositories
{
    public class AppUserRepository : AppUser, IAppUserRepository
    {
        private readonly DbSet<AppUser> _dbSet;
        private readonly IUserAccesor _userAccesor;

        public AppUserRepository(FinanceDBContext dbContext,IUserAccesor userAccesor)
        {
            _userAccesor = userAccesor;
            _dbSet = dbContext.Set<AppUser>();
        }

        public async Task<AppUser> GetUser()
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.UserName == _userAccesor.GetUsername());
        }
    }
}