using Library.Application.DTO.Books;

public interface IBookService
{
    Task<IEnumerable<BookDto>> GetAllAsync();
    Task<BookDto?> GetByIdAsync(int id);
    Task<int> AddAsync(CreateBookDto dto);
    Task UpdateAsync(int id, UpdateBookDto dto);
    Task DeleteAsync(int id);
}
