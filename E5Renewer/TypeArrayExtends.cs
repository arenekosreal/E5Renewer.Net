namespace E5Renewer
{
    /// <summary>Extends to <c>Type[]</c></summary>
    internal static class TypeArrayExtends
    {
        /// <summary>Get types which is not abstract and is assainabble to <typeparamref name="T">T</typeparamref>.</summary>
        /// <param name="types">The array of types.</param>
        /// <typeparam name="T">The target type to filter.</typeparam>
        public static IEnumerable<Type> GetNonAbstractClassesAssainableTo<T>(this Type[] types)
        {
            return types.Where((t) => !t.IsAbstract && t.IsAssignableTo(typeof(T)));
        }
    }
}
