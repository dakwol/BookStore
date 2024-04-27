using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class PublisherRepository(ApplicationContext context) : ICrudRepository<Publisher>
{
    public async Task<Publisher?> CreateAsync(Publisher publisher)
    {
        context.Publishers.Add(publisher);
        await context.SaveChangesAsync();
        return publisher;
    }

    public async Task<Publisher?> GetByIdAsync(int? id)
    {
        return await context.Publishers.FindAsync(id);
    }

    public async Task<IEnumerable<Publisher>?> GetAllAsync()
    {
        return await context.Publishers.ToListAsync();
    }

    public async Task<Publisher?> UpdateAsync(Publisher publisher)
    {
        context.Publishers.Update(publisher);
        await context.SaveChangesAsync();
        return publisher;
    }

    public async Task DeleteAsync(int id)
    {
        var publisher = await GetByIdAsync(id);
        if (publisher != null)
        {
            context.Publishers.Remove(publisher);
            await context.SaveChangesAsync();
        }
    }
}
