namespace E5Renewer
{
    internal static class StringExtends
    {
        public static FileInfo AsFileInfo(this string s) => new(s);
    }
}
