internal static class NullableExtends
{
    public static T EnsureNotNull<T>(this T? i, string? msg = default) => i is null ? throw new NullReferenceException(msg) : i;
}
