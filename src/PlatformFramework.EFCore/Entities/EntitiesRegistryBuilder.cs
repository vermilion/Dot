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
        /// Применение маппинга сущности на DTO
        /// </summary>
        /// <typeparam name="TDto">Тип DTO</typeparam>
        /// <param name="action">Метод конфигурирования</param>
        /// <param name="memberList">Тип валидации</param>
        public void MapToDto<TDto>(Action<IMappingExpression<TEntity, TDto>> action = null, MemberList memberList = MemberList.None)
            where TDto : class
        {
            Registry.RegisterMapping(expression =>
            {
                var map = expression.CreateMap<TEntity, TDto>(memberList);
                action?.Invoke(map);
            });
        }

        /// <summary>
        /// Применение маппинга DTO на  ущность
        /// </summary>
        /// <typeparam name="TDto">Тип DTO</typeparam>
        /// <param name="action">Метод конфигурирования</param>
        public void MapFromDto<TDto>(Action<IMappingExpression<TDto, TEntity>> action = null)
            where TDto : class
        {
            Registry.RegisterMapping(expression =>
            {
                var map = expression.CreateMap<TDto, TEntity>();
                action?.Invoke(map);
            });
        }

        /// <summary>
        /// Применение профиля для маппинга сущности
        /// </summary>
        /// <typeparam name="T">Тип профиля</typeparam>
        public void RegisterMappingProfile<T>()
            where T : Profile, new()
        {
            Registry.RegisterMapping(expression =>
            {
                expression.AddProfile<T>();
            });
        }
    }
}
