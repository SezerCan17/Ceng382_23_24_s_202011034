using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel.Design; // File sınıfı için gerekli olan kütüphane

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

        // JSON dosyasının okunması
        string jsonFilePath = "Data.json";
        string jsonString = File.ReadAllText(jsonFilePath);

        // JSON verisinin RoomData nesnesine dönüştürülmesi
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
                Console.WriteLine($"Room ID : {room.roomId}, Name:{room.roomName}, Capacity : {room.capacity}");
            }
        }

        string choice;

        do
        {
            Console.WriteLine("Choose:");
            Console.WriteLine("a) Add");
            Console.WriteLine("b) Delete");
            Console.WriteLine("c) Display");
            Console.WriteLine("d) Exit");
            choice = Console.ReadLine();

            switch (choice)
            {
                case "a":
                    Console.WriteLine("Write Date Time:");
                    DateTime date = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("Write Time:");
                    DateTime time = DateTime.Parse(Console.ReadLine());

                    int hour = time.Hour;
                    int idx = 0;

                    for (int i = 0; i < 16; i++)
                    {
                        if (i == hour)
                        {
                            idx = i;
                            Console.WriteLine(roomData.Rooms[i].roomName);
                        }
                    }

                    Console.WriteLine("Enter your name:");
                    string name = Console.ReadLine();
                    Console.WriteLine("Enter Room Id");
                    string Id = Console.ReadLine();

                    Reservation reservation = new Reservation(date, time, name, roomData.Rooms[idx]);
                    reservationHandler.addReservation(reservation);
                    break;

                case "b":
                    reservationHandler.deleteReservation();
                    break;

                case "c":
                    reservationHandler.displayWeeklySchedule();
                    break;

                case "d":
                    Console.WriteLine("Exiting...");
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

        } while (choice != "d");
    }
}

class ReservationHandler
{
    private Reservation[,] reservations = new Reservation[7, 16];

    public void addReservation(Reservation reservation)
    {
        int dayOfWeek = (int)reservation.date.DayOfWeek;
        int timeOfDay = reservation.time.Hour;
        reservations[dayOfWeek, timeOfDay] = reservation;
        Console.WriteLine("Reservation added successfully!");
    }

    public void deleteReservation()
    {
        Console.WriteLine("Enter Name: ");
        string name = Console.ReadLine();
        bool deleted = false;

        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                if (reservations[i, j] != null && reservations[i, j].reserverName == name)
                {
                    Console.WriteLine("Reservation found at: " + i + " " + j);
                    reservations[i, j] = null;
                    deleted = true;
                }
            }
        }

        if (deleted)
        {
            Console.WriteLine("Reservation deleted successfully!");
        }
        else
        {
            Console.WriteLine("No reservation found with that name.");
        }
    }

    public void displayWeeklySchedule()
    {
        string[] daysOfWeek = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

        for (int dayOfWeek = 0; dayOfWeek < 7; dayOfWeek++)
        {
            Console.WriteLine(daysOfWeek[dayOfWeek] + " Schedule:");

            for (int timeOfDay = 0; timeOfDay < 16; timeOfDay++)
            {
                Reservation reservation = reservations[dayOfWeek, timeOfDay];

                if (reservation != null)
                {
                    Console.WriteLine($"Time: {timeOfDay}:00 - {timeOfDay + 1}:00, Reserver: {reservation.reserverName}, Room: {reservation.room.roomName}");
                }
                else
                {
                    Console.WriteLine($"Time: {timeOfDay}:00 - {timeOfDay + 1}:00, Available");
                }
            }

            Console.WriteLine();
        }
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
