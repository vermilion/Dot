using System;
using AutoMapper;

namespace PlatformFramework.EFCore.Entities
{
    /// <summary>
    /// Fluent builder
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class EntitiesRegistryBuilder<TEntity>
        where TEntity : class
    {
        private EntitiesRegistry Registry { get; }

        internal EntitiesRegistryBuilder(EntitiesRegistry registry)
        {
            Registry = registry;
            RegisterDefaultServices();
        }

        private void RegisterDefaultServices()
        {
            Registry.RegisterServices(services =>
            {
            });
        }

        /// <summary>
        /// Use mapping to Model
        /// </summary>
        /// <typeparam name="TModel">Model type</typeparam>
        /// <param name="action">Configure method</param>
        /// <param name="memberList">Validation type</param>
        public void MapToDto<TModel>(Action<IMappingExpression<TEntity, TModel>> action = null, MemberList memberList = MemberList.None)
            where TModel : class
        {
            Registry.RegisterMapping(expression =>
            {
                var map = expression.CreateMap<TEntity, TModel>(memberList);
                action?.Invoke(map);
            });
        }

        /// <summary>
        /// Use mapping from Model
        /// </summary>
        /// <typeparam name="TModel">Model type</typeparam>
        /// <param name="action">Configure method</param>
        public void MapFromDto<TModel>(Action<IMappingExpression<TModel, TEntity>> action = null)
            where TModel : class
        {
            Registry.RegisterMapping(expression =>
            {
                var map = expression.CreateMap<TModel, TEntity>();
                action?.Invoke(map);
            });
        }

        /// <summary>
        /// Wrapper over Mapping profile
        /// </summary>
        /// <typeparam name="TProfile">Profile type</typeparam>
        public void RegisterMappingProfile<TProfile>()
            where TProfile : Profile, new()
        {
            Registry.RegisterMapping(expression =>
            {
                expression.AddProfile<TProfile>();
            });
        }
    }
}
