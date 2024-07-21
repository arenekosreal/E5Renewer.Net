namespace E5Renewer.Models.GraphAPIs
{
    /// <summary>Functions to extend <see cref="IEnumerable{T}"/>.</summary>
    public static class IEnumerableExtends
    {
        private static readonly Random random = new();

        /// <summary>Get different items by random weighted choice.</summary>
        /// <typeparam name="T">The type stored by the IEnumerable.</typeparam>
        /// <param name="sequence">The <see cref="IEnumerable{T}"/> to choose.</param>
        /// <param name="weightGenerator">The generator to get weight of item in <paramref name="sequence"/>.</param>
        /// <param name="count">How many items to choose from <paramref name="sequence"/>.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> which contains elements selected.</returns>
        public static IEnumerable<T> GetDifferentItemsByWeight<T>(this IEnumerable<T> sequence, Func<T, int> weightGenerator, int count)
        {
            List<T> items = sequence.ToList();

            for (int i = 0; i < count; i++)
            {
                int nextWeight = random.Next(0, items.Sum(weightGenerator));
                T selectedItem = items.First((item) => nextWeight < weightGenerator(item));
                yield return selectedItem;
                items.Remove(selectedItem);
            }
        }
    }
}
