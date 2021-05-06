using System.Collections.Generic;
using System.Threading.Tasks;
using Finance.Domain.Models;

namespace Finance.Domain.Interfaces
{
    public interface IAppUserRepository
    {
        Task<AppUser> GetUser();
    }
}