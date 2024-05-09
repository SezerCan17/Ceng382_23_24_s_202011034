using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Reservation
{
    // Foreign key
    public int Id { get; set; }

    [Required]
    public int ReservationId { get; set; }

    [Required]
    public int Roomid { get; set; }

    [Required]
    public DateTime ReservationDate { get; set; }

    [ForeignKey("Id")]
    public Room room { get; set; }
}