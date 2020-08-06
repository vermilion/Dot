using System;
using System.Collections.Generic;
using PlatformFramework.EFCore.Abstractions;

namespace PlatformFramework.EFCore.Domain
{
    public abstract class Entity : IEntity
    {
        private int? _hashCode;
        public virtual long Id { get; set; }
        protected virtual object This => this;

        public override int GetHashCode()
        {
            if (IsTransient()) return base.GetHashCode();

            if (!_hashCode.HasValue)
                _hashCode = (GetRealType().ToString() + Id).GetHashCode() ^ 31; // XOR for random distribution

            return _hashCode.Value;
        }

        public virtual bool IsTransient()
        {
            if (EqualityComparer<long>.Default.Equals(Id, default)) return true;

            //Workaround for EF Core since it sets int/long to min value when attaching to dbContext
            return Convert.ToInt64(Id) <= 0;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is Entity instance)) return false;

            if (ReferenceEquals(this, instance)) return true;

            if (GetRealType() != instance.GetRealType()) return false;

            if (IsTransient() || instance.IsTransient()) return false;

            return Id.Equals(instance.Id);
        }

        public override string ToString()
        {
            return $"[{GetRealType().Name}: {Id}]";
        }

        public static bool operator ==(Entity left, Entity right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }

        protected virtual Type GetRealType()
        {
            return This.GetType();
        }
    }
}