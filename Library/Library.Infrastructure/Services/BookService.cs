using Library.Application.DTO.Books;
using Library.Application.Interfaces.Services;
using Library.Domain.Entities;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class BookService : IBookService
{
    private readonly AppDbContext _context;

    public BookService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BookDto>> GetAllAsync() =>
        await _context.Books
            .Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                Genre = b.Genre,
                Year = b.Year
            })
            .ToListAsync();

    public async Task<BookDto?> GetByIdAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);
        return book == null ? null : new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Genre = book.Genre,
            Year = book.Year
        };
    }

    public async Task<int> AddAsync(CreateBookDto dto)
    {
        var book = new Book
        {
            Title = dto.Title,
            Author = dto.Author,
            Description = dto.Description ?? "Нет описания",
            Genre = dto.Genre,
            Year = dto.Year,
            AvailableCount = dto.AvailableCount > 0 ? dto.AvailableCount : 1,
            IsAvailable = true
        };


        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return book.Id;
    }

    public async Task UpdateAsync(int id, UpdateBookDto dto)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null) return;

        book.Title = dto.Title;
        book.Author = dto.Author;
        book.Genre = dto.Genre;
        book.Year = dto.Year;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book != null)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }
}
