namespace Library.Application.DTO.Reservations;

public class CreateReservationDto
{
    public int SeatId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
