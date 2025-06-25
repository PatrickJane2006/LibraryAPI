using Library.Application.DTO.Statistics;

public interface IStatisticsService
{
    Task<LibraryStatsDto> GetLibraryStatisticsAsync();
}
