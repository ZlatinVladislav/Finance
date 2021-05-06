using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Domain.Models.Base
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
    }
}
