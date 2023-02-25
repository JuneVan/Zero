namespace Zero.Application.Dtos
{
    public class ListResultDto<T>
    {
        /// <summary>
        /// 列表项集合
        /// </summary>
        public IReadOnlyList<T> Items
        {
            get { return _items ?? (_items = new List<T>()); }
            set { _items = value; }
        }
        private IReadOnlyList<T> _items;
        public ListResultDto()
        {

        }

        public ListResultDto(IReadOnlyList<T> items)
        {
            Items = items;
        }
    }
}
