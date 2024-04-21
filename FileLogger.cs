using System.IO;
using System.Text.Json;

public class FileLogger : ILogger
{
    private readonly string _filePath;

    public FileLogger(string filePath)
    {
        _filePath = filePath;
    }

    public void Log(LogRecord log)
    {
        var records = new List<LogRecord>();

        if (File.Exists(_filePath))
        {
            var json = File.ReadAllText(_filePath);
            records = JsonSerializer.Deserialize<List<LogRecord>>(json);
        }

        records.Add(log);
        var options = new JsonSerializerOptions { WriteIndented = true };
        var jsonString = JsonSerializer.Serialize(records, options);
        File.WriteAllText(_filePath, jsonString);
    }
}