using System;

namespace Floward.OnlineStore.Core.Models
{
    public abstract class EntityBase : ICloneable
    {
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
