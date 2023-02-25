namespace Zero.EntityFrameworkCore.Oracle
{
    public static class PropertyBuilderExtensions
    {
        /// <summary>
        /// 为字段指定值的增长序列
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="propertyBuilder"></param>
        /// <param name="sequenceName">序列名称</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static PropertyBuilder<TProperty> HasSequence<TProperty>(this PropertyBuilder<TProperty> propertyBuilder, string sequenceName)
        {
            if (string.IsNullOrEmpty(sequenceName)) throw new ArgumentNullException(nameof(sequenceName), "序列名不能为空。");
            propertyBuilder.Metadata.AddAnnotation("Sequence", sequenceName);
            return propertyBuilder;
        }
    }
}
