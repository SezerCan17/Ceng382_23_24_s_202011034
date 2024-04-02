using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class RoomData
{

    [JsonPropertyName("Room")]
    public Room[]? Rooms { get; set; }
}

public class Room
{


    [JsonPropertyName("roomId")]
    public string roomId { get; set; }

    [JsonPropertyName("roomName")]
    public string roomName { get; set; }



    [JsonPropertyName("capacity")]
    public int capacity { get; set; }

}


class Program
{

    static void Main(string[] args)
    {
        

        

        //path to json
        string jsonFilePath = "Data.json";
        string jsonString = File.ReadAllText(jsonFilePath);

        //options to read
        var options = new JsonSerializerOptions()
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString |
            JsonNumberHandling.WriteAsString
        };


        var roomData = JsonSerializer.Deserialize<RoomData>(jsonString, options);

        if (roomData?.Rooms != null)
        {
            foreach (var room in roomData.Rooms)
            {
                Console.WriteLine($"Room ID : {room.roomId}, Name:{room.roomName},Room ID : {room.roomId}");
            }
        }

        ReservationHandler reservationHandler;
        
        reservationHandler.addReservation(n);

    }
   
}


class ReservationHandler
{
    private Reservation[,] reservation = new Reservation[10,10];
    

    
    public void addReservation(Reservation reservation)
    {
        Console.WriteLine("SA");



    }

    public void deleteReservation(Reservation[,] reservation)
    {

    }

    public void displayWeeklySchedule()
    {

    }
}

class Reservation
{
    
   public DateTime time;
public    DateTime date;
   public string reserverName;

    Room room;
        
}


