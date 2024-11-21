using System.Net;

namespace E5Renewer
{
    internal static class StringExtends
    {
        public static FileInfo AsFileInfo(this string s) => new(s);

        public static IPEndPoint? AsIPEndPoint(this string s) => IPEndPoint.TryParse(s, out IPEndPoint? result) ? result : null;
    }
}
