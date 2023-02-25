namespace Zero.Domain.Auditing
{
    public abstract class FullAuditedEntity : AuditedEntity, IFullAudited
    {
        public virtual bool IsDeleted { get; set; }

        public virtual int? DeleterUserId { get; set; }

        public virtual DateTime? DeletedTime { get; set; }
    }
}