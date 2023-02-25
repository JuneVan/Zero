namespace Zero.Application.Dtos
{
    /// <summary>
    /// 树集合项DTO
    /// </summary>
    public class TreeItemDto
    {
        /// <summary>
        /// 值
        /// </summary>
        public virtual object Value { get; set; }
        /// <summary>
        /// 文本
        /// </summary>
        public virtual string Text { get; set; }
        /// <summary>
        /// 子集合
        /// </summary>
        public virtual List<TreeItemDto> Children { get; set; }
        public TreeItemDto() { Children = new List<TreeItemDto>(); }
        public TreeItemDto(object value, string text) : this()
        {
            Value = value;
            Text = text;
        }
    }
}
