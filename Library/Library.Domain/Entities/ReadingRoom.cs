namespace Library.Domain.Entities;

public class ReadingRoom
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Location { get; set; } = "";
    public int Capacity { get; set; }
    public bool IsAvailable { get; set; } = true;

    public ICollection<Seat> Seats { get; set; } = new List<Seat>();
}
