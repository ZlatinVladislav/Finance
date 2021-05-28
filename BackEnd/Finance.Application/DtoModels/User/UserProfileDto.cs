using System.Collections.Generic;
using Finance.Domain.Models;

namespace Finance.Application.DtoModels.User
{
    public class UserProfileDto
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Bio { get; set; }
        public string UserDescription { get; set; }
        public string Image { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}