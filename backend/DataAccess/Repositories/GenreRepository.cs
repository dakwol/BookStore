using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class GenreRepository(ApplicationContext context) : ICrudRepository<Genre>
{
    public async Task<Genre?> CreateAsync(Genre genre)
    {
        context.Genres.Add(genre);
        await context.SaveChangesAsync();
        return genre;
    }

    public async Task<Genre?> GetByIdAsync(int? id)
    {
        return await context.Genres.FindAsync(id);
    }

    public async Task<IEnumerable<Genre>?> GetAllAsync()
    {
        return await context.Genres.ToListAsync();
    }

    public async Task<Genre?> UpdateAsync(Genre genre)
    {
        context.Genres.Update(genre);
        await context.SaveChangesAsync();
        return genre;
    }

    public async Task DeleteAsync(int id)
    {
        var genre = await GetByIdAsync(id);
        if (genre != null)
        {
            context.Genres.Remove(genre);
            await context.SaveChangesAsync();
        }
    }
}
