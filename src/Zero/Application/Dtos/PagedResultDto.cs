namespace Zero.Application.Dtos
{
    public class PagedResultDto<T> : ListResultDto<T>
    {
        /// <summary>
        /// 数据总记录
        /// </summary>
        public virtual int TotalCount { get; set; }

        public PagedResultDto()
        {

        }
        public PagedResultDto(int totalCount, IReadOnlyList<T> items)
            : base(items)
        {
            TotalCount = totalCount;
        }
    }
}
