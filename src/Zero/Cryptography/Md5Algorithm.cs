namespace Zero.Cryptography
{
    /// <summary>
    /// 提供MD5加密功能
    /// </summary>
    public static class Md5Algorithm
    {
        public static string Encrypt(string plaintext)
        {
            MD5 md5 = MD5.Create();
            byte[] buffer = Encoding.UTF8.GetBytes(plaintext);
            byte[] md5Buffer = md5.ComputeHash(buffer);
            StringBuilder text = new StringBuilder();
            foreach (var item in md5Buffer)
                text.Append(item.ToString("x"));
            return text.ToString();
        }
    }
}
