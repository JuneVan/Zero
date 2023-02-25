namespace Zero.Events
{
    public interface IEntityChangeEventHelper
    {
        Task NotifyEntityChangeEvent(object entity);
        Task NotifyEntityCreatedEvent(object entity);
        Task NotifyUpdatedEvent(object entity);
        Task NotifyEntityDeletedEvent(object entity);
    }
}
