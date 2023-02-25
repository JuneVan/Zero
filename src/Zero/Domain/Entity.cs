namespace Zero.Domain
{
    /// <summary>
    /// 实体基类
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    public abstract class Entity : IEntity
    {
        public virtual int Id { get; set; }
    }
}
