
# Zero 框架系统

### 实体
```
public class Category : Entity
{
   /// <summary>
   /// 分类名称
   /// </summary>
   public string Name { get; set; }
}
```

### 仓储接口
```
public class CategoryService : ICategoryService
{
    private readonly IRepository<Category> _categoryRepository;
    public CategoryCache(IRepository<Category> categoryRepository)
    { 
        _categoryRepository = categoryRepository; 
    }
}

```

### 实体事件
``` 例如：处理缓存同步
internal class CategoryCacheSynchronizer : INotificationHandler<EntityChangedEvent<ProductUnit>>
{
    private readonly IDistributedCache _distributedCache;
    public CategoryCacheSynchronizer(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }
    public async Task Handle(EntityChangedEvent<ProductUnit> notification, CancellationToken cancellationToken)
    {
        // 订阅分类创建、更新、删除事件时清除缓存
        await _distributedCache.RemoveAsync(InfrastructureDefaults.CacheKeys.CategoryTree);
        await _distributedCache.RemoveAsync(string.Format(InfrastructureDefaults.CacheKeys.ParentCategoryNames, notification.Entity.Id)); 
    }
}
```
