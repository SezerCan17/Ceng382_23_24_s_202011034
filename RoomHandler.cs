using System.IO;
using System.Text.Json;

public class RoomHandler
{
    private readonly string _filePath;

    public RoomHandler(string filePath)
    {
        _filePath = filePath;
    }

    public IEnumerable<Room> GetRooms()
    {
        if (!File.Exists(_filePath)) return new List<Room>();

        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Room>>(json);
    }

    public void SaveRooms(IEnumerable<Room> rooms)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var jsonString = JsonSerializer.Serialize(rooms, options);
        File.WriteAllText(_filePath, jsonString);
    }
}