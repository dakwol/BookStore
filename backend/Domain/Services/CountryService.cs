using AutoMapper;
using DataAccess.Repositories;
using Domain.Interfaces;
using Domain.Models;

namespace Domain.Services;

public class CountryService(CountryRepository repository, IMapper mapper) : ICrudService<Country>
{
    public async Task<Country?> CreateAsync(Country dto)
    {
        var countryEntity = mapper.Map<DataAccess.Models.Country>(dto);
        var createdCountry = await repository.CreateAsync(countryEntity);
        return mapper.Map<Country>(createdCountry);
    }

    public async Task<Country?> GetByIdAsync(int id)
    {
        var countryEntity = await repository.GetByIdAsync(id);
        return mapper.Map<Country>(countryEntity);
    }

    public async Task<IEnumerable<Country>?> GetAllAsync()
    {
        var countryEntities = await repository.GetAllAsync();
        return mapper.Map<IEnumerable<Country>>(countryEntities);
    }

    public async Task<Country?> UpdateAsync(Country dto)
    {
        var countryEntity = mapper.Map<DataAccess.Models.Country>(dto);
        var updatedCountry = await repository.UpdateAsync(countryEntity);
        return mapper.Map<Country>(updatedCountry);
    }

    public async Task DeleteAsync(int id)
    {
        await repository.DeleteAsync(id);
    }
}
