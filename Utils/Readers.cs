namespace Utils;

public static class Readers
{
    public static string ReadAllInstructions(string filePath)
    {
        string text = string.Empty;
        try
        {
            using var sr = new StreamReader(filePath);
            text = sr.ReadToEnd();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        return text;
    }

    public static List<string> ReadInstructionLines(string filePath)
    {
        var lines = new List<string>();
        string? line = string.Empty;
        try
        {
            using var sr = new StreamReader(filePath);
            while ((line = sr.ReadLine()) != null)
            {
                lines.Add(line);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        return lines;
    }
}