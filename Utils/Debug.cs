using System.Text.Json;

namespace Utils;

public static class Debug
{
    /// <summary>
    /// Prints using the <see cref="Console.WriteLine"/>
    /// </summary>
    public static void Print<T>(this T o) => Console.WriteLine(Serialize(o));

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
