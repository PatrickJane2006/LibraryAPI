namespace Library.Application.DTO.Reservations;

public class ReservationDto
{
    public int Id { get; set; }
    public int SeatId { get; set; }
    public int SeatNumber { get; set; }
    public int RoomId { get; set; }
    public string RoomName { get; set; } = "";
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
