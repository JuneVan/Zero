namespace Zero.Cryptography
{
    /// <summary>
    /// 提供sha1算法
    /// </summary>
    public class SHA1Algorithm
    {
        /// <summary>
        /// 加密文本
        /// </summary>
        /// <param name="plaintext"></param>
        /// <returns></returns>
        public static string Encrypt(string plaintext)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(plaintext);
            SHA1 sha1 = SHA1.Create();
            var md5Buffer = sha1.ComputeHash(buffer);
            StringBuilder text = new StringBuilder();
            foreach (var item in md5Buffer)
                text.Append(item.ToString("X2"));
            return text.ToString();
        }
    }
}
