using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlatformFramework.EFCore.Entities.Customizers;

namespace PlatformFramework.EFCore.Entities
{
    /// <summary>
    /// Реестр сущностей
    /// </summary>
    public class EntitiesRegistry
    {
        private ConcurrentDictionary<Type, EntityCustomizer> EntityCustomizers { get; } = new ConcurrentDictionary<Type, EntityCustomizer>();
        private readonly List<Action<IServiceCollection>> _servicesActions = new List<Action<IServiceCollection>>();
        private readonly List<Action<MapperConfigurationExpression>> _mapperActions = new List<Action<MapperConfigurationExpression>>();

        internal void Configure(ModelBuilder modelBuilder)
        {
            foreach (var customizer in EntityCustomizers)
            {
                customizer.Value.Customize(modelBuilder, this);
            }
        }

        internal void ConfigureServices(IServiceCollection services)
        {
            foreach (var customizer in EntityCustomizers)
            {
                customizer.Value.Configure(this);
            }

            //services
            foreach (var action in _servicesActions)
            {
                action(services);
            }

            //automapper
            var expression = new MapperConfigurationExpression();

            foreach (var action in _mapperActions)
            {
                action(expression);
            }

            var automapperConfig = new MapperConfiguration(expression);
            services.AddSingleton(automapperConfig.CreateMapper());
        }

        public void RegisterCustomizer<TEntity, TEntityCustomizer>(TEntityCustomizer customizerInstance)
            where TEntity : class
            where TEntityCustomizer : EntityCustomizer
        {
            EntityCustomizers.TryAdd(typeof(TEntity), customizerInstance);
        }

        public EntityCustomizer GetCustomizer(Type type)
        {
            EntityCustomizers.TryGetValue(type, out var customizer);
            return customizer;
        }

        public void RegisterServices(Action<IServiceCollection> servicesAction)
        {
            _servicesActions.Add(servicesAction);
        }

        public void RegisterMapping(Action<MapperConfigurationExpression> mapperAction)
        {
            _mapperActions.Add(mapperAction);
        }
    }
}
