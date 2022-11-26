namespace Utils
{
    public static class Mappers
    {
        public static string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.ASCII.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            StringBuilder sBuilder = new StringBuilder();
            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            // Return the hexadecimal string.
            // return sBuilder.ToString();

            return Convert.ToHexString(data);
        }

        public static IEnumerable<T> Map<TSource, T>(this IEnumerable<TSource> source, Func<TSource, T> map)
        => source.Select(i => map(i));
    }
}