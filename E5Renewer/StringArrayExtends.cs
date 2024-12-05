namespace E5Renewer
{
    internal static class StringArrayExtends
    {
        public static bool ContainsFlag(this string[] args, string flag, string prefix)
        {
            if (flag.Length <= 0)
            {
                throw new InvalidDataException($"{nameof(flag.Length)} of {nameof(flag)} is not greater than 0");
            }
            bool allPrefixes = flag.Chunk(prefix.Length)
                .All((cs) => prefix.Contains(string.Concat(cs)));
            if (allPrefixes)
            {
                throw new InvalidDataException($"{nameof(flag)} is invalid");
            }
            return args.Contains($"{prefix}{flag}");
        }
    }
}
