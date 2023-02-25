namespace Zero.Caching.Redis
{
    public interface IDistributedLocker
    {
        /// <summary>
        /// 获取分布式锁
        /// </summary>
        /// <param name="key">设置锁键名</param>
        /// <param name="lockExpirySeconds">设置锁时间</param>
        /// <param name="waitLockSeconds">设置获取锁超时时间</param>
        /// <returns></returns>
        Task<bool> TryLockAsync(string key, int lockExpirySeconds = 10, long waitLockSeconds = 60);
        /// <summary>
        /// 释放锁
        /// </summary>
        /// <param name="key">设置锁键名</param>
        /// <returns></returns>
        Task UnlockAsync(string key);
        /// <summary>
        /// 获取锁并执行业务逻辑
        /// </summary>
        /// <param name="key">设置锁键名</param>
        /// <param name="handle">执行业务逻辑</param>
        /// <param name="lockExpirySeconds">设置锁时间</param>
        /// <param name="waitLockSeconds">设置获取锁超时时间</param>
        /// <returns></returns>
        Task LockAndExecuteAsync(string key, Func<Task> handle, int lockExpirySeconds = 10, long waitLockSeconds = 60);
    }
}
