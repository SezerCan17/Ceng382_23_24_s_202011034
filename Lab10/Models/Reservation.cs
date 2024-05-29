using System;
using System.Collections.Generic;

namespace Lab10.Models;

public partial class Reservation
{
    public int Id { get; set; }

    public int ReservationId { get; set; }

    public int RoomId { get; set; }

    public DateTime ReservationDate { get; set; }

    public virtual Room Room { get; set; } = null!;
}
