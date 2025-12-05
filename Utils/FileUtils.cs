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
            Logger.LogLine(
                $"[FileUtils.ReadTextFileAsync] An error occurred while reading the file: {ex.Message}"
            );
        }

        return content;
    }

    public static async Task<List<T>> ReadListFromFileAsync<T>(string filePath, char[] separators)
        where T : Common.IReadableString<T>
    {
        Logger.LogLine($"[FileUtils.ReadTextFileAsync<{typeof(T).Name}>] Reading file: {filePath}");
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
            Logger.LogLine($"An error occurred while reading the file: {ex.Message}");
        }

        return results;
    }

    public static async Task<List<T>> ReadListFromFileAsync<T>(string filePath, char separator)
        where T : Common.IReadableString<T>
    {
        return await ReadListFromFileAsync<T>(filePath, new[] { separator });
    }

    public static async Task<List<string>> ReadListFromFileAsync(string filePath, char[] separators)
    {
        Logger.LogLine($"[FileUtils.ReadTextFileAsync<string>] Reading file: {filePath}");
        var results = new List<string>();
        try
        {
            string content = await File.ReadAllTextAsync(filePath);

            content
                .Split(separators, StringSplitOptions.RemoveEmptyEntries)
                .ToList()
                .ForEach(line =>
                {
                    results.Add(line);
                });
        }
        catch (Exception ex)
        {
            Logger.LogLine($"An error occurred while reading the file: {ex.Message}");
        }

        return results;
    }
}
