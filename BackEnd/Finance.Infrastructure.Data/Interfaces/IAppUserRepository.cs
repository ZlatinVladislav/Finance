using System.Threading.Tasks;
using Finance.Domain.Models;

namespace Finance.Infrastructure.Data.Interfaces
{
    public interface IAppUserRepository
    {
        Task<AppUser> GetUser();
        Task<AppUser> GetUserPhoto();
        Task<AppUser> GetUserByUrlUsername(string userName);
    }
}