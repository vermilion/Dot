using System.Collections.Generic;

namespace PlatformFramework.Models
{
    /// <summary>
    /// Base Framework model class
    /// </summary>
    public abstract class ReadModel
    {
        public int Id { get; set; }

        public virtual bool IsNew() => EqualityComparer<int>.Default.Equals(Id, default);
    }
}
