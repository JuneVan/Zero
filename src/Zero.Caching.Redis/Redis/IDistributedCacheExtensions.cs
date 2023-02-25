namespace Microsoft.Extensions.Caching.Distributed
{
    public static class IDistributedCacheExtensions
    {


        /// <summary>
        /// 获取缓存，反序列化成对象返回
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key">key</param>
        /// <returns>对象</returns>
        public static object GetObject(this IDistributedCache cache, string key)
        {
            return Deserialize<object>(cache.Get(key));
        }
        /// <summary>
        /// 获取缓存，反序列化成对象返回
        /// </summary>
        /// <typeparam name="T">反序列化类型</typeparam>
        /// <param name="cache"></param>
        /// <param name="key">key</param>
        /// <returns>对象</returns>
        public static T GetObject<T>(this IDistributedCache cache, string key)
        {
            var obj = Deserialize<T>(cache.Get(key));
            if (obj == null) return default(T);
            return obj;
        }
        /// <summary>
        /// 获取缓存，反序列化成对象
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key">key</param>
        /// <returns>对象</returns>
        public async static Task<object> GetObjectAsync(this IDistributedCache cache, string key)
        {
            return Deserialize<object>(await cache.GetAsync(key));
        }
        /// <summary>
        /// 获取缓存，反序列化成对象
        /// </summary>
        /// <typeparam name="T">反序列化类型</typeparam>
        /// <param name="cache"></param>
        /// <param name="key">key</param>
        /// <returns>对象</returns>
        public async static Task<T> GetObjectAsync<T>(this IDistributedCache cache, string key)
        {
            return Deserialize<T>(await cache.GetAsync(key));
        }
        /// <summary>
        /// 序列化对象后，设置缓存
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key">key</param>
        /// <param name="value">对象</param>
        public static void SetObject(this IDistributedCache cache, string key, object value)
        {
            var data = Serialize(value);
            if (data == null) cache.Remove(key);
            else cache.Set(key, Serialize(value));
        }
        /// <summary>
        /// 序列化对象后，设置缓存
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key">key</param>
        /// <param name="value">对象</param>
        /// <param name="options">策略</param>
        public static void SetObject(this IDistributedCache cache, string key, object value, DistributedCacheEntryOptions options)
        {
            var data = Serialize(value);
            if (data == null) cache.Remove(key);
            else cache.Set(key, Serialize(value), options);
        }
        /// <summary>
        /// 序列化对象后，设置缓存
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key">key</param>
        /// <param name="value">对象</param>
        public static Task SetObjectAsync(this IDistributedCache cache, string key, object value)
        {
            var data = Serialize(value);
            if (data == null) return cache.RemoveAsync(key);
            else return cache.SetAsync(key, Serialize(value));
        }
        /// <summary>
        /// 序列化对象后，设置缓存
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key">key</param>
        /// <param name="value">对象</param>
        /// <param name="options">策略</param>
        public static Task SetObjectAsync(this IDistributedCache cache, string key, object value, DistributedCacheEntryOptions options)
        {
            var data = Serialize(value);
            if (data == null) return cache.RemoveAsync(key);
            else return cache.SetAsync(key, Serialize(value), options);
        }

        /// <summary>
        /// 获取缓存数据，当缓存不存在时，通过工厂方法创建缓存
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="cache">分布式缓存接口</param>
        /// <param name="key">缓存键</param>
        /// <param name="factory">创建缓存工厂</param>
        /// <returns></returns>
        public static async Task<T> GetOrCreateAsync<T>(this IDistributedCache cache, string key, Func<DistributedCacheEntryOptions, Task<T>> factory)
        {
            T data = await cache.GetObjectAsync<T>(key);
            if (data == null)
            {
                var options = new DistributedCacheEntryOptions();
                data = await factory?.Invoke(options);
                if (data != null)
                    await cache.SetObjectAsync(key, data, options);
            }
            return data;
        }
        private static byte[] Serialize(object value)
        {
            if (value == null) return null;
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value));
        }
        private static T Deserialize<T>(byte[] buffer)
        {
            if (buffer == null) return default;
            return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(buffer));
        }
    }
}
