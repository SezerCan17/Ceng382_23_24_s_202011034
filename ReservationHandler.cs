using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReservationSystem
{
    public class ReservationHandler
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly RoomHandler _roomHandler;

        public ReservationHandler(IReservationRepository reservationRepository, RoomHandler roomHandler)
        {
            _reservationRepository = reservationRepository;
            _roomHandler = roomHandler;
        }

        public void AddReservation(DateTime time, DateTime date, string reserverName, string roomName)
        {
            var room = _roomHandler.GetRooms()
                .FirstOrDefault(r => r.Name.Equals(roomName, StringComparison.OrdinalIgnoreCase));

           

            var reservation = new Reservation(time, date, reserverName, room);
            _reservationRepository.AddReservation(reservation);
        }

        public void DeleteReservation(Reservation reservation)
        {
            _reservationRepository.DeleteReservation(reservation);
        }

        public IEnumerable<Reservation> GetAllReservations()
        {
            return _reservationRepository.GetAllReservations();
        }

        public IEnumerable<Room> GetAllRooms()
        {
            return _roomHandler.GetRooms();
        }

        public string GetRoomName(int roomId)
        {
            var room = _roomHandler.GetRooms().FirstOrDefault(r => r.Id == roomId);
            return room != null ? room.Name : string.Empty;
        }
    }
}
