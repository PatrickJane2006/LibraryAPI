﻿using Library.Domain.Entities;

public class Reservation
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int SeatId { get; set; }            
    public Seat Seat { get; set; } = null!;    

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
