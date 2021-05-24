using System;

namespace Finance.Domain.Models.Base
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
    }
}