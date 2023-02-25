namespace Zero.Domain
{
    public interface IRepository<TEntity>
       where TEntity : IEntity
    {
        IUnitOfWork UnitOfWork { get; }
        /// <summary>
        /// 添加实体记录
        /// </summary>
        /// <param name="entity"></param>
        Task<TEntity> InsertAsync(TEntity entity);
        /// <summary>
        /// 添加实体记录并返回主键
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> InsertAndGetIdAsync(TEntity entity);
        /// <summary>
        /// 添加或更新实体实体记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> InsertOrUpdateAsync(TEntity entity);
        /// <summary>
        /// 更新实体记录
        /// </summary>
        /// <param name="entity"></param>
        Task<TEntity> UpdateAsync(TEntity entity);
        /// <summary>
        /// 删除实体记录
        /// </summary>
        /// <param name="entity"></param>
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// 通过指定的id删除实体记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(int id);
        /// <summary>
        /// 通过指定的id查询第一条或默认的实体，不存在时返回NULL
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync(int id);
        /// <summary>
        /// 通过指定的id查询第一条或默认的实体，并加载关联实体对象，不存在时返回NULL 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="propertySelectors"></param>
        /// <returns></returns>
        Task<TEntity> IncludingFirstOrDefaultAsync(int id, params Expression<Func<TEntity, object>>[] propertySelectors);
        /// <summary>
        /// 通过指定条件查询第一条或默认的实体，不存在时返回NULL
        /// </summary>
        /// <param name="expression">条件表达式</param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 通过指定条件查询第一条或默认的实体，并加载关联实体对象，不存在时返回NULL 
        /// 用法示例：IncludingFirstOrDefaultAsync(f => f.Id == id, f => f.Specifications, f => f.Images)
        /// </summary>
        /// <param name="expression">条件表达式</param>
        /// <param name="propertySelectors">加载对象</param>
        /// <returns></returns>
        Task<TEntity> IncludingFirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] propertySelectors);
        /// <summary>
        /// 通过指定条件统计记录数量
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 获取实体Linq表达式
        /// </summary>
        /// <param name="propertySelectors">指定包含(Include)的实体对象</param>
        /// <returns></returns>
        IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] propertySelectors);
        /// <summary>
        /// 获取实体查询的所有记录列表
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> GetAllListAsync();
        /// <summary>
        /// 获取实体查询条件结果记录列表
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> expression);

    }
}
