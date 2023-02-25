namespace Zero.Domain
{
    public class EntityNotFoundException : ZeroException, IHasLogLevel
    {
        public EntityNotFoundException(string message)
            : base(message)
        {
        }
        public EntityNotFoundException(Type entityType, object id)
            : this(entityType, id, null)
        {
            EntityType = entityType;
            Id = id;
        }
        public EntityNotFoundException(Type entityType, object id, Exception innerException)
            : base($"无法找到实体类型`{entityType.Name}`- `{id}`的记录", innerException)
        {

        }
        public Type EntityType { get; set; }
        public object Id { get; set; }

        public LogLevel Level => LogLevel.Information;
    }
}
