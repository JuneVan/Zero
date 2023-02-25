namespace Zero.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        void RegisterNew<TEntity>(TEntity entity)
                    where TEntity : class, IEntity;
        void RegisterModified<TEntity>(TEntity entity)
           where TEntity : class, IEntity;
        void RegisterDeleted<TEntity>(TEntity entity)
           where TEntity : class, IEntity;
        Task<int> CommitAsync();
    }
}
