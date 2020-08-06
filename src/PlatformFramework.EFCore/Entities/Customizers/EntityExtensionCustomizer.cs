using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlatformFramework.EFCore.Context;
using System;

namespace PlatformFramework.EFCore.Entities.Customizers
{
    /// <summary>
    /// Кастомайзер для расширений сущности
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class EntityExtensionBuilder<TEntity> : FluentBuilderBase<EntityTypeBuilder<TEntity>>
        where TEntity : class
    {
        private readonly EntityCustomizer<TEntity> _customizer;

        public EntityExtensionBuilder(EntityCustomizer<TEntity> customizer)
        {
            _customizer = customizer;
        }

        /// <summary>
        /// Включение Optimistic Concurrency check по столбцу RowVersion
        /// </summary>
        /// TODO: должны ли передавать RowVersion на клиент, чтобы держать там версию
        /// <returns></returns>
        public EntityExtensionBuilder<TEntity> AddConcurrencyCheck()
        {
            _customizer.Flags.Set(EfCore.IsConcurrencyCheckEnabled);

            AddAction(modelBuilder =>
            {
                modelBuilder.Property<byte[]>(EfCore.RowVersion).IsRowVersion();
            });

            return this;
        }

        public EntityExtensionBuilder<TEntity> AddSoftDelete()
        {
            _customizer.Flags.Set(EfCore.IsSoftDeleteEnabled);

            AddAction(modelBuilder =>
            {
                modelBuilder
                    .Property<bool>(EfCore.IsDeleted);

                modelBuilder.HasQueryFilter(f => EF.Property<bool>(f, EfCore.IsDeleted) == false);
            });

            return this;
        }

        /// <summary>
        /// Аудит удаления сущности из БД
        /// </summary>
        /// <returns>Fluent</returns>
        public EntityExtensionBuilder<TEntity> AddDeletionTracking()
        {
            _customizer.Flags.Set(EfCore.IsDeletionTrackingEnabled);

            AddAction(modelBuilder =>
            {
                modelBuilder
                    .Property<DateTime?>(EfCore.DeletedDateTime)
                    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

                modelBuilder
                    .Property<long?>(EfCore.DeletedByUserId)
                    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            });

            return this;
        }

        /// <summary>
        /// Аудит добавления сущности в БД
        /// </summary>
        /// <returns>Fluent</returns>
        public EntityExtensionBuilder<TEntity> AddCreationTracking()
        {
            _customizer.Flags.Set(EfCore.IsCreationTrackingEnabled);

            AddAction(modelBuilder =>
            {
                modelBuilder
                   .Property<DateTime>(EfCore.CreatedDateTime)
                   .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

                modelBuilder
                    .Property<long>(EfCore.CreatedByUserId)
                    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            });

            return this;
        }

        /// <summary>
        /// Аудит редактирования сущности в БД
        /// </summary>
        /// <returns>Fluent</returns>
        public EntityExtensionBuilder<TEntity> AddModificationTracking()
        {
            _customizer.Flags.Set(EfCore.IsModificationTrackingEnabled);

            AddAction(modelBuilder =>
            {
                modelBuilder
                    .Property<DateTime?>(EfCore.ModifiedDateTime);

                modelBuilder
                   .Property<long?>(EfCore.ModifiedByUserId);
            });

            return this;
        }

        /*

        public EntityExtensionBuilder<TEntity> VersionEnabled(bool? value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Кэширование запросов сущности
        /// </summary>
        /// <returns>Fluent</returns>
        public EntityExtensionBuilder<TEntity> CachingEnabled()
        {
            throw new NotImplementedException();
        }*/
    }
}
