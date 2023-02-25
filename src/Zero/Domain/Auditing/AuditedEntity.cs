namespace Zero.Domain.Auditing
{
    public abstract class AuditedEntity : CreationAuditedEntity, IAudited, IEntity
    {
        public virtual DateTime? LastModifiedTime { get; set; }
        public virtual int? LastModifierUserId { get; set; }
    }
}