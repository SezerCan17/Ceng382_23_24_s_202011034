using System.Collections.Generic;

public class ReservationRepository : IReservationRepository
{
    private readonly List<Reservation> _reservations;

    public ReservationRepository()
    {
        _reservations = new List<Reservation>();
    }

    public void AddReservation(Reservation reservation)
    {
        _reservations.Add(reservation);
    }

    public void DeleteReservation(Reservation reservation)
    {
        _reservations.Remove(reservation);
    }

    public IEnumerable<Reservation> GetAllReservations()
    {
        return _reservations;
    }
}