public readonly struct Room
{
    public string Id { get; }
    public string Name { get; }
    public int Capacity { get; }

    public Room(string id, string name, int capacity)
    {
        Id = id;
        Name = name;
        Capacity = capacity;
    }
}