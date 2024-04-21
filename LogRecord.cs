public readonly struct LogRecord
{
    public DateTime Timestamp { get; }
    public string ReserverName { get; }
    public string RoomName { get; }

    public LogRecord(DateTime timestamp, string reserverName, string roomName)
    {
        Timestamp = timestamp;
        ReserverName = reserverName;
        RoomName = roomName;
    }
}