using AutoMapper;
using DataAccess.Repositories;
using Domain.Interfaces;
using Domain.Models;

namespace Domain.Services;

public class GenreService(GenreRepository repository, IMapper mapper) : ICrudService<Genre>
{ 
    public async Task<Genre?> CreateAsync(Genre dto)
    {
        var genreEntity = mapper.Map<DataAccess.Models.Genre>(dto);
        var createdGenre = await repository.CreateAsync(genreEntity);
        return mapper.Map<Genre>(createdGenre);
    }

    public async Task<Genre?> GetByIdAsync(int id)
    {
        var genreEntity = await repository.GetByIdAsync(id);
        return mapper.Map<Genre>(genreEntity);
    }

    public async Task<IEnumerable<Genre>?> GetAllAsync()
    {
        var genreEntities = await repository.GetAllAsync();
        return mapper.Map<IEnumerable<Genre>>(genreEntities);
    }

    public async Task<Genre?> UpdateAsync(Genre dto)
    {
        var genreEntity = mapper.Map<DataAccess.Models.Genre>(dto);
        var updatedGenre = await repository.UpdateAsync(genreEntity);
        return mapper.Map<Genre>(updatedGenre);
    }

    public async Task DeleteAsync(int id)
    {
        await repository.DeleteAsync(id);
    }
}