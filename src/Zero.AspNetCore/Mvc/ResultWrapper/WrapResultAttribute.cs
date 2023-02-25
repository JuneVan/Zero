namespace Zero.AspNetCore.Mvc.ResultWrapper
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method)]
    public class WrapResultAttribute : Attribute
    {
        public bool Enable { get; private set; }
        public WrapResultAttribute()
        {
            Enable = true;
        }
        public WrapResultAttribute(bool enable)
        {
            Enable = enable;
        }
        public static WrapResultAttribute DefaultWrapResult => new();

    }
}
