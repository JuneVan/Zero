namespace Zero.Events
{
    public class EntityChangeEventHelper : IEntityChangeEventHelper
    {
        private readonly IPublisher _publisher;
        private readonly IThreadSignal _signal;
        private readonly ILogger _logger;
        public EntityChangeEventHelper(IPublisher publisher,
             IThreadSignal signal,
             ILogger<EntityChangeEventHelper> logger)
        {
            _publisher = publisher;
            _signal = signal;
            _logger = logger;
        }
        public async Task NotifyEntityChangeEvent(object entity)
        {
            await NotifyEventWithEntity(typeof(EntityChangedEvent<>), entity);
        }
        public async Task NotifyEntityCreatedEvent(object entity)
        {
            await NotifyEventWithEntity(typeof(EntityCreatedEvent<>), entity);
        }
        public async Task NotifyUpdatedEvent(object entity)
        {
            await NotifyEventWithEntity(typeof(EntityUpdatedEvent<>), entity);
        }
        public async Task NotifyEntityDeletedEvent(object entity)
        {
            await NotifyEventWithEntity(typeof(EntityDeletedEvent<>), entity);
        }
        protected virtual async Task NotifyEventWithEntity(Type genericEventType, object entity)
        {
            _logger.LogDebug(message: $"发布实体事件`{entity.GetType()}`-`{genericEventType.Name}`。");
            Type entityType = entity.GetType();
            Type eventType = genericEventType.MakeGenericType(entityType);
            INotification @event = Activator.CreateInstance(eventType, new[] { entity }) as INotification;
            if (@event != null)
                await _publisher.Publish(@event, _signal.Token);
        }

    }
}
