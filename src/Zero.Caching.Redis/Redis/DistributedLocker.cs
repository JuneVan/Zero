namespace Zero.Caching.Redis
{
    public class DistributedLocker : IDistributedLocker
    {
        public async Task<bool> TryLockAsync(string key, int lockExpirySeconds = 10, long waitLockSeconds = 60)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("锁键值不能为空。");

            DateTime begin = DateTime.Now;
            while (true)
            {
                if (await RedisHelper.SetAsync(GetLockKey(key), Thread.CurrentThread.ManagedThreadId, lockExpirySeconds, RedisExistence.Nx))
                {
                    return true;
                }
                //不等待锁则返回
                if (waitLockSeconds == 0)
                {
                    break;
                }

                //超过等待时间，则不再等待
                if ((DateTime.Now - begin).TotalSeconds >= waitLockSeconds)
                {
                    break;
                }

                Thread.Sleep(100);
            }
            return false;
        }
        public async Task UnlockAsync(string key) => await RedisHelper.DelAsync(GetLockKey(key));
        public async Task LockAndExecuteAsync(string key, Func<Task> handle, int lockExpirySeconds = 10, long waitLockSeconds = 60)
        {
            if (await TryLockAsync(key, lockExpirySeconds, waitLockSeconds))
            {
                try
                {
                    await handle?.Invoke();
                }
                finally
                {
                    await UnlockAsync(key);
                }
            }
        }
        private string GetLockKey(string key) => $"Kalami.Lock.{key}";
    }
}
