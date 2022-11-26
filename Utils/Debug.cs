namespace Utils;

public static class Debug
{
    /// <summary>
    /// Prints using serialization and the <see cref="Console.WriteLine"/>
    /// </summary>
    public static void PrintUsingSerialization<T>(this T o) => Console.WriteLine(Serialize(o));

    /// <summary>
    /// Prints using the <see cref="Console.WriteLine"/>
    /// </summary>
    public static void Print<T>(this T o) => Console.WriteLine(o);

    /// <summary>
    /// Prints each element of the <paramref name="sequence"/> on a new line using serialization.
    /// </summary>
    /// <param name="sequence"></param>
    public static void PrintEachUsingSerialization<T>(this IEnumerable<T> sequence) => sequence.ToList().ForEach(o => o.PrintUsingSerialization());

    /// <summary>
    /// Prints each element of the <paramref name="sequence"/> on a new line.
    /// </summary>
    /// <param name="sequence"></param>
    public static void PrintEach<T>(this IEnumerable<T> sequence) => sequence.ToList().ForEach(o => o.Print());

    /// <summary>
    /// Serializes an object of type <typeparamref name="T"/>
    /// </summary>
    public static string Serialize<T>(this T o) => JsonSerializer.Serialize(o);
}
