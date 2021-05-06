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
        private readonly FinanceDBContext _context;

        private readonly IUserAccesor _userAccesor;
        // private readonly DbSet<Transaction> _dbSet;

        public AppUserRepository(FinanceDBContext dbContext,IUserAccesor userAccesor)
        {
            _context = dbContext;
            _userAccesor = userAccesor;
            //_dbSet = dbContext.Set<TEntity>();
        }

        public async Task<AppUser> GetUser()
        {
            return await _context.Set<AppUser>().FirstOrDefaultAsync(x => x.UserName == _userAccesor.GetUsername());
        }
    }
}