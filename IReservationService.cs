

public interface IReservationService
{
    void AddReservation(DateTime time, DateTime date, string reserverName, string roomName);
    void DeleteReservation(Reservation reservation);
    // Add more methods for reservation service operations
}
