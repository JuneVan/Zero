namespace Zero.EntityFrameworkCore
{
    public abstract class EntityTypeConfigurationBase<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : class, IEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasComment("Id");

            if (typeof(IConcurrencyToken).IsAssignableFrom(typeof(TEntity)))
            {
                builder.Property("ConcurrencyStamp").IsConcurrencyToken();
            }

            // 添加注释
            AppendPropertyComments(builder);
        }
        private void AppendPropertyComments(EntityTypeBuilder<TEntity> builder)
        {
            if (typeof(IHasCreationTime).IsAssignableFrom(typeof(TEntity)))
            {
                builder.Property("CreatedOnUtc").HasComment("创建时间");
                if (typeof(ICreationAudited).IsAssignableFrom(typeof(TEntity)))
                    builder.Property("CreatorUserId").HasComment("创建人的用户Id");
            }

            if (typeof(IHasModificationTime).IsAssignableFrom(typeof(TEntity)))
            {
                builder.Property("LastModifiedOnUtc").HasComment("最后更新时间");
                if (typeof(IModificationAudited).IsAssignableFrom(typeof(TEntity)))
                    builder.Property("LastModifierUserId").HasComment("最后更新人的用户Id");
            }

            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                builder.Property("IsDeleted").HasComment("是否已被删除");
                if (typeof(IHasDeletionTime).IsAssignableFrom(typeof(TEntity)))
                {
                    builder.Property("DeletedOnUtc").HasComment("删除时间");
                    if (typeof(IDeletionAudited).IsAssignableFrom(typeof(TEntity)))
                        builder.Property("DeleterUserId").HasComment("删除人的用户Id");
                }
            }
        }
    }

}
