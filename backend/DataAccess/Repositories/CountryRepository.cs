using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class CountryRepository(ApplicationContext context) : ICrudRepository<Country>
{
    public async Task<Country?> CreateAsync(Country country)
    {
        context.Add(country);
        await context.SaveChangesAsync();
        return country;
    }

    public async Task<Country?> GetByIdAsync(int? id)
    {
        return await context.Countries.SingleOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Country>?> GetAllAsync()
    {
        return await context.Countries.
            Include(c => c.Authors).
            ToListAsync();
    }

    public async Task<Country?> UpdateAsync(Country country)
    {
        context.Update(country);
        await context.SaveChangesAsync();
        return country;
    }

    public async Task DeleteAsync(int id)
    {
        var country = await GetByIdAsync(id);
        if (country != null)
        {
            context.Countries.Remove(country);
            await context.SaveChangesAsync();
        }
    }
}
