namespace Utils
{
    public static class Mappers
    {
        public static IEnumerable<T> Map<TSource, T>(
            this IEnumerable<TSource> source, Func<TSource, T> map) =>
            source.Select(i => map(i));
    }
}