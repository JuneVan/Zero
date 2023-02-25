namespace Zero.Events
{
    public class EntityDeletedEvent<TEntity> : EntityChangedEvent<TEntity>
    {
        public EntityDeletedEvent(TEntity entity)
            : base(entity)
        {

        }
    }
}
