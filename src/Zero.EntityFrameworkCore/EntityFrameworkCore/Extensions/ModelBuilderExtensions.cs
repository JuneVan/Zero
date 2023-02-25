namespace Zero.EntityFrameworkCore.Extensions
{
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// 过滤已被逻辑删除的实体记录
        /// </summary>
        /// <param name="modelBuilder"></param>
        internal static void ApplyFilterDeletedEntity(this ModelBuilder modelBuilder)
        {
            foreach (Microsoft.EntityFrameworkCore.Metadata.IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes()
                .Where(e => typeof(ISoftDelete).IsAssignableFrom(e.ClrType)))
            {
                modelBuilder.Entity(entityType.ClrType).Property<bool>("IsDeleted");
                ParameterExpression parameter = Expression.Parameter(entityType.ClrType, "e");
                BinaryExpression body = Expression.Equal(
                    Expression.Call(typeof(EF), nameof(EF.Property), new[] { typeof(bool) }, parameter, Expression.Constant("IsDeleted")),
                Expression.Constant(false));
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(Expression.Lambda(body, parameter));
            }
        }
    }
}
