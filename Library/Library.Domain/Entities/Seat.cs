namespace Library.Domain.Entities;

public class Seat
{
    public int Id { get; set; }
    public int ReadingRoomId { get; set; }
    public ReadingRoom ReadingRoom { get; set; } = null!;
    public int Number { get; set; }
    public bool IsOccupied { get; set; } = false;

    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
