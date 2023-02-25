namespace Zero.Events
{
    public class EntityUpdatedEvent<TEntity> : EntityChangedEvent<TEntity>
    {
        public EntityUpdatedEvent(TEntity entity)
            : base(entity)
        {

        }
    }
}
