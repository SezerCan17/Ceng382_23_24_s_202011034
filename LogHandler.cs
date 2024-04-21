using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
public class LogHandler
{
    private readonly ILogger _logger;

    public LogHandler(ILogger logger)
    {
        _logger = logger;
    }

    public void AddLog(LogRecord log)
    {
        _logger.Log(log);
    }

    public List<LogRecord> GetAllLogs()
    {
        using var jsonFile = File.OpenText("LogData.json");
        return JsonSerializer.Deserialize<List<LogRecord>>(jsonFile.ReadToEnd(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public void SaveLogs(List<LogRecord> logs)
    {
        using var outputStream = File.OpenWrite("LogData.json");
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true, PropertyNameCaseInsensitive = true };
        JsonSerializer.Serialize(outputStream, logs, jsonOptions);
    }
}