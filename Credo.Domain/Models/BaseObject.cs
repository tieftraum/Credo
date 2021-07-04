using System;

namespace Credo.Domain.Models
{
    public abstract class BaseObject
    {
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt => DateTime.Now;
    }
}
