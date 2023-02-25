using Zero.Reflections;
using Desc = System.ComponentModel.DescriptionAttribute;

namespace Zero.UnitTest.Reflections
{
    internal class EnumHelperTest
    {
        [Test]
        public void GetEnumArray()
        {
            var colors = EnumHelper.GetEnumArray<Color>();
        }
    }
    public enum Color
    {
        [Desc("红色")]
        Red,
        Blue
    }
}
