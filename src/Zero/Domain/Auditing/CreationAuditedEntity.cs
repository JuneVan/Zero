namespace Zero.Domain.Auditing
{
    public abstract class CreationAuditedEntity : Entity, ICreationAudited
    {
        protected CreationAuditedEntity()
        {
            CreatedTime = DateTime.Now;
        }
        public virtual DateTime CreatedTime { get; set; }

        public virtual int CreatorUserId { get; set; }

    }
}