using System;
using System.Collections.Generic;
using System.Text;

namespace ReservationSystem
{
    public class ReservationService : IReservationService
    {
        private readonly ReservationHandler _reservationHandler;

        public ReservationService(ReservationHandler reservationHandler)
        {
            _reservationHandler = reservationHandler;
        }

        public void AddReservation(DateTime time, DateTime date, string reserverName, string roomName)
        {
            _reservationHandler.AddReservation(time, date, reserverName, roomName);
        }

        public void DeleteReservation(Reservation reservation)
        {
            _reservationHandler.DeleteReservation(reservation);
        }

        public string Display()
        {
            var reservations = _reservationHandler.GetAllReservations();
            var rooms = _reservationHandler.GetAllRooms();

            var displayString = new StringBuilder();

            displayString.AppendLine("Rooms:");
            foreach (var room in rooms)
            {
                displayString.AppendLine($"- {room.Name} (Capacity: {room.Capacity})");
            }

            displayString.AppendLine("\nReservations:");
            foreach (var reservation in reservations)
            {
                var roomName = _reservationHandler.GetRoomName(reservation.Room.Id);
                displayString.AppendLine($"- {reservation.ReserverName} - {roomName} ({reservation.Time} {reservation.Date})");
            }

            return displayString.ToString();
        }
    }
}
