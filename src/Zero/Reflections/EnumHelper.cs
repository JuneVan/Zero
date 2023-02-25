namespace Zero.Reflections
{
    public static class EnumHelper
    {
        public static Dictionary<int, string> GetEnumArray<TEnum>()
        {
            var keyValues = new Dictionary<int, string>();
            foreach (FieldInfo fieldInfo in typeof(TEnum).GetFields())
            {
                var propertyName = fieldInfo.Name;
                if (propertyName == "value__")
                    continue;
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes.Length > 0)
                    propertyName = attributes[0].Description;
                var propertyValue = fieldInfo.GetValue(null);
                keyValues.Add(Convert.ToInt32(propertyValue), propertyName);
            }
            return keyValues;
        }
    }
}
