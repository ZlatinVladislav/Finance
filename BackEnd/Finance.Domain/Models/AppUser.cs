using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finance.Domain.Models
{
    public class AppUser:IdentityUser
    {
        public string DisplayName { get; set; }
        public string Bio { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
