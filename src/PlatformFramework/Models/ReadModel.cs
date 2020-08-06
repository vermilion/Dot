using System.Collections.Generic;

namespace PlatformFramework.Domain.Models
{
    public abstract class ReadModel
    {
        public long Id { get; set; }

        public virtual bool IsNew() => EqualityComparer<long>.Default.Equals(Id, default);
    }
}
