﻿namespace PlatformFramework.EFCore.Eventing.Requests
{
    /// <summary>
    /// Base delete entity request type
    /// </summary>
    /// <typeparam name="TReadModel">Model</typeparam>
    public abstract class EntityDeleteRequest<TReadModel> : EntityIdentifierRequest<TReadModel>
    {
        public EntityDeleteRequest(long id) : base(id)
        {
        }
    }
}
