using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class AuthorRepository(ApplicationContext context) : ICrudRepository<Author>
{
    public async Task<Author?> CreateAsync(Author author)
    {
        
        context.Authors.Add(author);
        await context.SaveChangesAsync();
        return author;
    }

    public async Task<Author?> GetByIdAsync(int? id)
    {
        return await context.Authors.SingleOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<Author>?> GetAllAsync()
    {
        return await context.Authors.
            Include(a => a.Country).
            ToListAsync();
    }

    public async Task<Author?> UpdateAsync(Author author)
    {
        context.Authors.Update(author);
        await context.SaveChangesAsync();
        return author;
    }

    public async Task DeleteAsync(int id)
    {
        var author = await GetByIdAsync(id);
        if (author != null)
        {
            context.Authors.Remove(author);
            await context.SaveChangesAsync();
        }
    }
}
