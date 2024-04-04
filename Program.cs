using System;
using System.IO;
using System.Text.Json;

public class RoomData
{
    public Room[]? Rooms { get; set; }
}

public class Room
{
    public string RoomId { get; set; }
    public string RoomName { get; set; }
    public int Capacity { get; set; }
}

class Program
{
    enum DayOfWeekEnum
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

    static void Main(string[] args)
    {
        ReservationHandler reservationHandler = new ReservationHandler();

        string jsonFilePath = "Data.json";
        string jsonString = File.ReadAllText(jsonFilePath);

        var roomData = JsonSerializer.Deserialize<RoomData>(jsonString);

        if (roomData?.Rooms != null)
        {
            foreach (var room in roomData.Rooms)
            {
                Console.WriteLine($"Room ID: {room.RoomId}, Name: {room.RoomName}, Capacity: {room.Capacity}");
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
                    AddReservation(reservationHandler, roomData);
                    break;

                case "b":
                    reservationHandler.DeleteReservation();
                    break;

                case "c":
                    reservationHandler.DisplayWeeklySchedule();
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

    static void AddReservation(ReservationHandler reservationHandler, RoomData roomData)
    {
        Console.WriteLine("Enter Reservation Day (Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday): ");
        string dayInput = Console.ReadLine();

        Console.WriteLine("Enter Reservation Hours ('..:..' , between 00:00 and 16:00): ");
        DateTime time = DateTime.Parse(Console.ReadLine());

        DayOfWeekEnum dayOfWeek = (DayOfWeekEnum)Enum.Parse(typeof(DayOfWeekEnum), dayInput, true);

        DateTime date = GetDateFromDayOfWeek(dayOfWeek);

        int hour = time.Hour;

        if (hour >= 0 && hour < 16)
        {
            int idx = 0;

            for (int i = 0; i < 16; i++)
            {
                if (i == hour)
                {
                    idx = i;
                    Console.WriteLine(roomData.Rooms[i].RoomName);
                }
            }

            Console.WriteLine("Enter your name:");
            string name = Console.ReadLine();

            reservationHandler.AddReservation(new Reservation(date, time, name, roomData.Rooms[idx]));

        }
        else
        {
            Console.WriteLine("I can make reservations between 00:00 and 16:00.");
        }
    }

    static DateTime GetDateFromDayOfWeek(DayOfWeekEnum dayOfWeek)
    {
        DateTime currentDate = DateTime.Today;
        int daysUntilRequestedDay = ((int)dayOfWeek - (int)currentDate.DayOfWeek + 7) % 7;
        DateTime requestedDate = currentDate.AddDays(daysUntilRequestedDay);
        return requestedDate;
    }
}

class ReservationHandler
{
    private Reservation[,] reservations = new Reservation[7, 16];

    public void AddReservation(Reservation reservation)
    {
        int dayOfWeek = (int)reservation.Date.DayOfWeek;
        int timeOfDay = reservation.Time.Hour;
        if (reservations[dayOfWeek, timeOfDay] != null)
        {
            Console.WriteLine("There is already a reservation at that date and time. Reservation cannot be added.");
            return;
        }
        else
        {
            reservations[dayOfWeek, timeOfDay] = reservation;
            Console.WriteLine("Reservation added successfully!");

        }

    }

    public void DeleteReservation()
    {
        Console.WriteLine("Enter Name: ");
        string name = Console.ReadLine();
        bool deleted = false;

        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                if (reservations[i, j] != null && reservations[i, j].ReserverName == name)
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

    public void DisplayWeeklySchedule()
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
                    Console.WriteLine($"Time: {timeOfDay}:00 - {timeOfDay + 1}:00, Reserver: {reservation.ReserverName}, Room: {reservation.Room.RoomName}");
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
    public DateTime Time;
    public DateTime Date;
    public string ReserverName;
    public Room Room;

    public Reservation(DateTime date_, DateTime time_, string reserverName_, Room room_)
    {
        Time = time_;
        Date = date_;
        ReserverName = reserverName_;
        Room = room_;
    }
}
