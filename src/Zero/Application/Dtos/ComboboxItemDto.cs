namespace Zero.Application.Dtos
{
    /// <summary>
    /// 下拉选项DTO
    /// </summary>
    public class ComboboxItemDto
    {
        public virtual object Value { get; set; }
        public virtual string Text { get; set; }
        public ComboboxItemDto() { }
        public ComboboxItemDto(object value, string text)
        {
            Value = value;
            Text = text;
        }
    }
}
