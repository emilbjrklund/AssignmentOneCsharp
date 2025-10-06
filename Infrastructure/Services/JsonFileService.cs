using ProductApp.Infrastructure.Interfaces;

namespace ProductApp.Infrastructure.Services;

public class JsonFileService(string filePath) : IFileService
{
    private readonly string _filePath = filePath;

    public string GetContentFromFile()
    {
        if (!File.Exists(_filePath)) return string.Empty;
        return File.ReadAllText(_filePath);
    }

    public bool SaveToFile(string content)
    {
        try
        {
            var directory = Path.GetDirectoryName(_filePath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            File.WriteAllText(_filePath, content);
            return true;
        }
        catch
        {
            return false;
        }

    }
}