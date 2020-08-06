using System.Collections.Generic;

namespace PlatformFramework.Domain.Models
{
    /// <summary>
    /// Base Framework model class
    /// </summary>
    public abstract class ReadModel
    {
        public long Id { get; set; }

        public virtual bool IsNew() => EqualityComparer<long>.Default.Equals(Id, default);
    }
}
