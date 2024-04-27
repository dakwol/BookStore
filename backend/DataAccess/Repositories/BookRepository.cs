using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class BookRepository(ApplicationContext context) : ICrudRepository<Book>
{
    public async Task<Book?> CreateAsync(Book book)
    {
        context.Books.Add(book);
        await context.SaveChangesAsync();
        return book;
    }

    public async Task<Book?> GetByIdAsync(int? id)
    {
        return await context.Books
            .Include(b => b.Author)
            .ThenInclude(a => a!.Country)
            .Include(b => b.Genre)
            .Include(b => b.Publisher)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<IEnumerable<Book>?> GetAllAsync()
    {
        return await context.Books
            .Include(b => b.Author)
            .ThenInclude(a => a!.Country)
            .Include(b => b.Genre)
            .Include(b => b.Publisher)
            .ToListAsync();
    }

    public async Task<Book?> UpdateAsync(Book book)
    {
        context.Books.Update(book);
        await context.SaveChangesAsync();
        return book;
    }

    public async Task DeleteAsync(int id)
    {
        var book = await GetByIdAsync(id);
        if (book != null)
        {
            context.Books.Remove(book);
            await context.SaveChangesAsync();
        }
    }
}
