using Finance.Domain.Models;

namespace Finance.Application.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}