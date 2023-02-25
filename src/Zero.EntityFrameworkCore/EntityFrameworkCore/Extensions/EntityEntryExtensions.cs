namespace Zero.EntityFrameworkCore.Extensions
{
    public static class EntityEntryExtensions
    {
        public static void ApplyCreationAuditedEntity(this EntityEntry entry, int? userId)
        {
            if (entry.Entity is not IHasCreationTime hasCreation)
                return;
            hasCreation.CreatedTime = DateTime.Now;

            if (entry.Entity is not ICreationAudited creationAudited)
                return;
            if (userId.HasValue)
                creationAudited.CreatorUserId = userId.Value;
        }

        public static void ApplyModificationAuditedEntity(this EntityEntry entry, int? userId)
        {
            if (entry.Entity is not IHasModificationTime hasModification)
                return;
            hasModification.LastModifiedTime = DateTime.Now;

            if (entry.Entity is not IModificationAudited modificationAudited)
                return;
            if (userId.HasValue)
                modificationAudited.LastModifierUserId = userId.Value;
        }

        public static void ApplyDeletionAuditedEntity(this EntityEntry entry, int? userId)
        {
            if (entry.Entity is not ISoftDelete softDeleteEntity)
                return;

            entry.Reload();
            entry.State = EntityState.Modified;
            softDeleteEntity.IsDeleted = true;

            if (entry.Entity is not IHasDeletionTime hasDeletion)
                return;

            hasDeletion.DeletedTime = DateTime.Now;

            if (entry.Entity is not IDeletionAudited deletionAudited)
                return;
            if (userId.HasValue)
                deletionAudited.DeleterUserId = userId.Value;
        }
    }
}
