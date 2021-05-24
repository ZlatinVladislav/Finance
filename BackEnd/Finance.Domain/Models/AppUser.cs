using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Finance.Domain.Models
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public string Bio { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public Guid? UserDescriptionId { get; set; }
        public UserDescription UserDescription { get; set; }
    }
}