public interface IReadingRoomService
{
    Task<int> CreateAsync(CreateReadingRoomDto dto);
    Task<IEnumerable<ReadingRoomDto>> GetAllAsync();
}