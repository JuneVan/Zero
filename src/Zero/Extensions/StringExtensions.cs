namespace Zero.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string str)
        {
            if (str == null) return true;
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNullOrWhiteSpace(this string str)
        {
            if (str == null) return true;
            return string.IsNullOrWhiteSpace(str);
        }

        public static long ToLong(this string strValue)
        {
            if (strValue == null)
            {
                return 0;
            }

            if (long.TryParse(strValue.ToString(), out long value))
            {
                return value;
            }

            return 0L;
        }


        public static DateTime ToDate(this string dateTimeStr)
        {
            if (dateTimeStr.IsNullOrEmpty())
                return DateTime.MinValue;

            if (DateTime.TryParse(dateTimeStr, out DateTime value))
                return value;

            return DateTime.MinValue;
        }


        public static int ToInt(this string str)
        {
            if (int.TryParse(str, out int value))
                return value;
            return 0;
        }

        public static string Left(this string str, int len)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));

            if (str.Length < len)
                return str;

            return str.Substring(0, len);
        }

        public static string Right(this string str, int len)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));

            if (str.Length < len)
                return str;

            return str.Substring(str.Length - len, len);
        }

        public static string RemovePostfix(this string str, params string[] postfixes)
        {
            if (str == null)
                return null;

            if (string.IsNullOrEmpty(str))
                return string.Empty;

            if (postfixes.IsNullOrEmpty())
                return str;

            foreach (string text in postfixes)
                if (str.EndsWith(text))
                    return str.Left(str.Length - text.Length);

            return str;
        }
        public static string RemovePrefix(this string str, params string[] prefixes)
        {
            if (str == null)
                return null;

            if (string.IsNullOrEmpty(str))
                return string.Empty;

            if (prefixes.IsNullOrEmpty())
                return str;
            foreach (string text in prefixes)
                if (str.StartsWith(text))
                    return str.Right(str.Length - text.Length);
            return str;
        }



        public static string[] Split(this string str, string separator)
        {
            if (str == null)
                return Array.Empty<string>();
            return str.Split(new[] { separator }, StringSplitOptions.None);
        }


        public static string[] Split(this string str, string separator, StringSplitOptions options)
        {
            if (str == null)
                return Array.Empty<string>();
            return str.Split(new[] { separator }, options);
        }


        public static string ToCamelCase(this string str, bool invariantCulture = true)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;

            if (str.Length == 1)
                return invariantCulture ? str.ToLowerInvariant() : str.ToLower();

            return (invariantCulture ? char.ToLowerInvariant(str[0]) : char.ToLower(str[0])) + str.Substring(1);
        }

        public static T ToEnum<T>(this string value)
            where T : struct
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return (T)Enum.Parse(typeof(T), value);
        }

        public static T ToEnum<T>(this string value, bool ignoreCase)
            where T : struct
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return (T)Enum.Parse(typeof(T), value, ignoreCase);
        }

        public static string ToMd5(this string str)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(str);
                var hashBytes = md5.ComputeHash(inputBytes);

                var sb = new StringBuilder();
                foreach (var hashByte in hashBytes)
                {
                    sb.Append(hashByte.ToString("X2"));
                }

                return sb.ToString();
            }
        }
    }
}