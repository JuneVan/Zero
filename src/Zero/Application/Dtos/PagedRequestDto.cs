namespace Zero.Application.Dtos
{
    public class PagedRequestDto
    {
        /// <summary>
        /// 排序字段 默认为时间降序
        /// 例如 时间降序为"CreatedTime DESC"
        /// </summary>
        [DefaultValue("CreatedTime DESC")]
        public virtual string Sorting { get; set; } = "CreatedTime DESC";
        /// <summary>
        /// 每页数据的数量
        /// </summary>
        [DefaultValue(10)]
        public virtual int PageSize { get; set; } = 10;
        /// <summary>
        /// 当前页码值 默认为1
        /// </summary>
        [DefaultValue(1)]
        public virtual int PageIndex { get; set; } = 1;
    }
}
