namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddZeroEntityFrameworkCore<TDbContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> configure)
          where TDbContext : EfCoreDbContext<TDbContext>
        {
            // 查找所有的是实体类型
            var entityTypes = from property in typeof(TDbContext).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                              where
                                  ReflectionHelper.IsAssignableToType(property.PropertyType, typeof(DbSet<>)) &&
                                  property.PropertyType.GenericTypeArguments.Length > 0 &&
                                  ReflectionHelper.IsAssignableToType(property.PropertyType.GenericTypeArguments[0],
                                      typeof(IEntity))
                              select property.PropertyType.GenericTypeArguments[0];
            // 注册仓储服务类及接口
            foreach (var entityType in entityTypes)
            {
                services.AddScoped(typeof(IRepository<>).MakeGenericType(entityType), typeof(EfCoreRepository<,>).MakeGenericType(typeof(TDbContext), entityType));
            }
            services.AddScoped(typeof(IUnitOfWork), typeof(EfCoreUnitOfWork<>).MakeGenericType(typeof(TDbContext)));

            services.AddDbContext<TDbContext>(configure);
            return services;
        }
    }
}
