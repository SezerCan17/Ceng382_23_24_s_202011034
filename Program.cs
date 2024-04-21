using System;

namespace ReservationSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize room data
            var roomHandler = new RoomHandler("rooms.json");
            var rooms = roomHandler.GetRooms();

            // Initialize reservation data
            var reservationRepository = new ReservationRepository();

            // Initialize reservation handler
            var reservationHandler = new ReservationHandler(reservationRepository, roomHandler);

            // Initialize logger
            var logger = new FileLogger("logs.json");
            var logHandler = new LogHandler(logger);

            // Initialize reservation service
            var reservationService = new ReservationService(reservationHandler);

            // Perform reservation operations
            reservationService.AddReservation(DateTime.Now, DateTime.Now.AddDays(1), "John Doe", "Room A");

            // Log reservation actions
            logHandler.AddLog(new LogRecord(DateTime.Now, "John Doe", "Room A"));

            // Save reservations and rooms to JSON files
            roomHandler.SaveRooms(rooms);
            // Save reservations to JSON files
            // ...

            // Display reservations and rooms
            Console.WriteLine(reservationService.Display());
        }
    }
}
