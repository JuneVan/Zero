namespace Zero.EntityFrameworkCore
{
    public class EfCoreRepository<TDbContext, TEntity> : IRepository<TEntity>
       where TEntity : class, IEntity
        where TDbContext : DbContext
    {
        private bool disposedValue;
        private readonly IThreadSignal _signal;
        public EfCoreRepository(IUnitOfWork unitOfWork,
            TDbContext context,
            IThreadSignal signal)
        {
            Context = context;
            UnitOfWork = unitOfWork;
            _signal = signal;
        }
        protected TDbContext Context { get; }
        public IUnitOfWork UnitOfWork { get; }
        protected CancellationToken CancellationToken => _signal.Token;
        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            UnitOfWork.RegisterNew(entity);
            await Task.CompletedTask;
            return entity;
        }
        public async Task<int> InsertAndGetIdAsync(TEntity entity)
        {
            await InsertAsync(entity);
            await Context.SaveChangesAsync(CancellationToken);
            return entity.Id;
        }
        public async Task<TEntity> InsertOrUpdateAsync(TEntity entity)
        {
            if (entity.Id == default)
            {
                return await InsertAsync(entity);
            }
            else
            {
                return await UpdateAsync(entity);
            }
        }
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            UnitOfWork.RegisterModified(entity);
            await Task.CompletedTask;
            return entity;
        }
        public async Task DeleteAsync(TEntity entity)
        {
            UnitOfWork.RegisterDeleted(entity);
            await Task.CompletedTask;
        }
        public async Task DeleteAsync(int id)
        {
            var entry = Context.ChangeTracker.Entries()
               .FirstOrDefault(
                   entry =>
                       entry.Entity is TEntity &&
                       EqualityComparer<int>.Default.Equals(id, ((TEntity)entry.Entity).Id)
               );

            if (entry.Entity is not TEntity entity)
                return;
            await DeleteAsync(entity);
        }
        public virtual async Task<TEntity> FirstOrDefaultAsync(int id)
        {
            return await Context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id, CancellationToken);
        }
        public virtual async Task<TEntity> IncludingFirstOrDefaultAsync(int id, params Expression<Func<TEntity, object>>[] propertySelectors)
        {

            IQueryable<TEntity> query = Context.Set<TEntity>().AsQueryable();

            if (propertySelectors != null)
            {
                foreach (Expression<Func<TEntity, object>> propertySelector in propertySelectors)
                {
                    query = query.Include(propertySelector);
                }
            }
            return await query.FirstOrDefaultAsync(x => x.Id == id, CancellationToken);
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
        {

            return await Context.Set<TEntity>().FirstOrDefaultAsync(expression, CancellationToken);
        }

        public async Task<TEntity> IncludingFirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] propertySelectors)
        {

            IQueryable<TEntity> query = Context.Set<TEntity>().AsQueryable();

            if (propertySelectors != null)
            {
                foreach (Expression<Func<TEntity, object>> propertySelector in propertySelectors)
                {
                    query = query.Include(propertySelector);
                }
            }
            return await query.FirstOrDefaultAsync(expression, CancellationToken);
        }
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> expression)
        {

            return await Context.Set<TEntity>().CountAsync(expression, CancellationToken);
        }
        public IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>().AsQueryable();

            if (propertySelectors != null)
            {
                foreach (Expression<Func<TEntity, object>> propertySelector in propertySelectors)
                {
                    query = query.Include(propertySelector);
                }
            }
            return query;
        }
        public async Task<List<TEntity>> GetAllListAsync()
        {
            return await GetAll()
                .ToListAsync(CancellationToken);
        }
        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await GetAll()
                .Where(expression)
                .ToListAsync(CancellationToken);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}