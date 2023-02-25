namespace Zero.Events
{

    public class EntityChangedEvent : INotification
    {
        public Dictionary<string, string> Metadata { get; }
        public EntityChangedEvent()
        {
            Metadata = new Dictionary<string, string>();
        }
        public void AddMetadata(string key, string value)
        {
            if (!Metadata.TryAdd(key, value))
            {
                throw new ArgumentException($"添加键`{key}`-值`{value}`失败。");
            }
        }
    }
    public class EntityChangedEvent<TEntity> : EntityChangedEvent
    {
        public EntityChangedEvent(TEntity entity)
        {
            Entity = entity;

        }
        public TEntity Entity { get; private set; }

    }
}
