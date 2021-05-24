using Finance.Domain.Models;
using Finance.Infrastructure.Data.Interfaces.Base;

namespace Finance.Infrastructure.Data.Interfaces
{
    public interface IBankRepository : IBaseRepository<Bank>
    {
    }
}