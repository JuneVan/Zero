namespace Zero.EntityFrameworkCore.Oracle
{
    public class OracleEfCoreDbContext<TDbContext> : EfCoreDbContext<TDbContext>
        where TDbContext : DbContext
    {
        public OracleEfCoreDbContext(DbContextOptions options, IServiceProvider serviceProvider)
            : base(options, serviceProvider)
        {

        }



        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            SetSequenceValue();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            SetSequenceValue();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        /// <summary>
        /// 设置字段增长序列
        /// </summary>
        protected virtual void SetSequenceValue()
        {
            var addedEntries = ChangeTracker.Entries().Where(w => w.State == EntityState.Added).ToList();
            if (addedEntries != null)
            {
                foreach (var entry in addedEntries)
                {
                    foreach (var propertyEntry in entry.Properties)
                    {
                        var sequence = propertyEntry.Metadata.FindAnnotation("Sequence");
                        if (sequence != null)
                            propertyEntry.CurrentValue = GetSequenceValue(Schema, sequence.Value?.ToString());
                    }
                }
            }
        }
        private int GetSequenceValue(string schema, string sequence)
        {
            var connection = Database.GetDbConnection();
            var command = connection.CreateCommand();
            if (connection.State != ConnectionState.Open) connection.Open();
            command.CommandText = $"select {schema}.{sequence}.NEXTVAL from dual";
            return Convert.ToInt32(command.ExecuteScalar());
        }
    }
}
