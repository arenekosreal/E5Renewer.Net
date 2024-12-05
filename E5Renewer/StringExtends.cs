namespace E5Renewer
{
    internal static class StringExtends
    {
        public static FileInfo AsFileInfo(this string s) => new(s);

        public static bool ContainsAny(this string s, IEnumerable<string> items) => items.Any((i) => s.Contains(i));
    }
}
