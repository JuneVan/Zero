namespace Zero
{
    public class Singleton<T>
    {
        private static T instance;
        static Singleton()
        {
            AllSingletons = new Dictionary<Type, object>();
        }
        public static IDictionary<Type, object> AllSingletons { get; }
        public static T Instance
        {
            get
            {
                return instance;
            }
            set
            {
                instance = value;
                AllSingletons[typeof(T)] = value;
            }
        }
    }
}
