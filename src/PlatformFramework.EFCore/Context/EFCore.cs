namespace PlatformFramework.EFCore.Context
{
    public static class EfCore
    {
        public const string CreatedDateTime = nameof(CreatedDateTime);
        public const string CreatedByUserId = nameof(CreatedByUserId);

        public const string ModifiedDateTime = nameof(ModifiedDateTime);
        public const string ModifiedByUserId = nameof(ModifiedByUserId);

        public const string UserId = nameof(UserId);

        public const string IsDeleted = nameof(IsDeleted);
        public const string DeletedDateTime = nameof(DeletedDateTime);
        public const string DeletedByUserId = nameof(DeletedByUserId);

        public const string IsSoftDeleteEnabled = nameof(IsSoftDeleteEnabled);
        public const string IsCreationTrackingEnabled = nameof(IsCreationTrackingEnabled);
        public const string IsModificationTrackingEnabled = nameof(IsModificationTrackingEnabled);
        public const string IsDeletionTrackingEnabled = nameof(IsDeletionTrackingEnabled);

        public const string IsConcurrencyCheckEnabled = nameof(IsConcurrencyCheckEnabled);
        public const string RowVersion = nameof(RowVersion);
    }
}