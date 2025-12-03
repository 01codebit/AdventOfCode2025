namespace Utils;

public static class FileUtils
{
    public static async Task<string> ReadTextFileAsync(string path)
    {
        string content = "";
        try
        {
            content = await File.ReadAllTextAsync(path);
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"[FileUtils.ReadTextFileAsync] An error occurred while reading the file: {ex.Message}"
            );
        }

        return content;
    }

    public static async Task<List<T>> ReadListFromFileAsync<T>(
        string filePath,
        char[] separators,
        bool debug = false
    )
        where T : Common.IReadableString<T>
    {
        Console.WriteLine(
            $"[FileUtils.ReadTextFileAsync<{typeof(T).Name}>] Reading file: {filePath}"
        );
        var results = new List<T>();
        try
        {
            string content = await File.ReadAllTextAsync(filePath);

            content
                .Split(separators, StringSplitOptions.RemoveEmptyEntries)
                .ToList()
                .ForEach(line =>
                {
                    T obj = T.FromString(line);
                    results.Add(obj);
                });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
        }

        return results;
    }

    public static async Task<List<T>> ReadListFromFileAsync<T>(
        string filePath,
        char separator,
        bool debug = false
    )
        where T : Common.IReadableString<T>
    {
        return await ReadListFromFileAsync<T>(filePath, new[] { separator }, debug);
    }
}
