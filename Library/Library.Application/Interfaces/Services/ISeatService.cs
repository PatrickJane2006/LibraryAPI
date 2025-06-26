public interface ISeatService
{
    Task<int> CreateAsync(CreateSeatDto dto);
    Task<IEnumerable<SeatDto>> GetByRoomIdAsync(int roomId);
}