using System;
using System.ComponentModel;
using System.Diagnostics;
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
        ReservationHandler reservationHandler = new ReservationHandler();

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
                Console.WriteLine($"Room ID : {room.roomId}, Name:{room.roomName},Capacity : {room.capacity}");
            }
        }

        Console.WriteLine("Choose:");
        Console.WriteLine("a) Add");
        Console.WriteLine("b) Delete");
        Console.WriteLine("c) Display");
        string a = Console.ReadLine();



        switch (a)
        {
            case "a":
                Console.WriteLine("Write Date Time:");
                DateTime date = DateTime.Parse(Console.ReadLine());
                Console.WriteLine(date);
                Console.WriteLine("Write Time:");
                DateTime time = DateTime.Parse(Console.ReadLine());
                Console.WriteLine(time);

                Console.WriteLine("Enter your name:");
                string name = Console.ReadLine();
                Console.WriteLine(name);

                Console.WriteLine("Enter Room Id");
                string Id = Console.ReadLine();

                if (roomData?.Rooms != null)
                {
                    foreach (var room_Id in roomData.Rooms)
                    {
                        Console.WriteLine(room_Id.roomId + "This is room ID");
                        if (room_Id.roomId == Id)
                        {
                            break;

                        }
                        else
                        {
                            Console.WriteLine("Don't have this Id");
                        }
                    }
                }

                Console.WriteLine(Id + "BUUUU!!!");

                Console.WriteLine("Enter Room name:");
                string rName = Console.ReadLine();

                if(roomData?.Rooms != null)
                {
                    foreach (var room_name in roomData.Rooms)
                    {
                        Console.WriteLine(room_name.roomName+ "This is room name");
                        if (room_name.roomName == rName)
                        {
                            break;

                        }
                        else
                        {
                            Console.WriteLine("Don't have this name");
                        }
                    }
                }

                Console.WriteLine(rName + "BUUUU!!!");

                



                break;

            case "b":
                break;

            case "c":
                break;

            default:
                break;
        }





        //reservationHandler.addReservation(reservation1);
        //reservationHandler.deleteReservation(reservation1);


    }
}


class ReservationHandler
{
    private Reservation[,] reservations = new Reservation[7, 24];

    public void addReservation(Reservation reservation)
    {
        int dayOfWeek = (int)reservation.date.DayOfWeek;
        int timeOfDay = reservation.time.Hour;
        reservations[dayOfWeek, timeOfDay] = reservation;

    }

    public void deleteReservation(Reservation reservation)
    {
        int dayOfWeek = (int)reservation.date.DayOfWeek;
        int timeOfDay = reservation.time.Hour;
        reservations[dayOfWeek, timeOfDay] = null;

        Console.WriteLine(reservations[dayOfWeek, timeOfDay]);

    }

    public void displayWeeklySchedule()
    {

    }
}

class Reservation
{
    public DateTime time;
    public DateTime date;
    public string reserverName;
    public Room room;

    public Reservation(DateTime date_, DateTime time_, string reserverName_, Room room_)
    {
        time = time_;
        date = date_;
        reserverName = reserverName_;
        room = room_;
    }



}


