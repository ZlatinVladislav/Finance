using Finance.Domain.Models.Base;

namespace Finance.Domain.Models
{
    public class UserDescription: BaseEntity
    {
        public string UserDescriptionId { get; set; }
        public string Description { get; set; }
        public virtual AppUser User { get; set; }
    }
}