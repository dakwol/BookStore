using AutoMapper;
using DataAccess.Repositories;
using Domain.Interfaces;
using Domain.Models;

namespace Domain.Services;

public class AuthorService(AuthorRepository repository, IMapper mapper) : ICrudService<Author>
{
    public async Task<Author?> CreateAsync(Author dto)
    {
        var authorEntity = mapper.Map<DataAccess.Models.Author>(dto);
        var createdAuthor = await repository.CreateAsync(authorEntity);
        return mapper.Map<Author>(createdAuthor);
    }

    public async Task<Author?> GetByIdAsync(int id)
    {
        var authorEntity = await repository.GetByIdAsync(id);
        return mapper.Map<Author>(authorEntity);
    }

    public async Task<IEnumerable<Author>?> GetAllAsync()
    {
        var authorEntities = await repository.GetAllAsync();
        return mapper.Map<IEnumerable<Author>>(authorEntities);
    }

    public async Task<Author?> UpdateAsync(Author dto)
    {
        var authorEntity = mapper.Map<DataAccess.Models.Author>(dto);
        var updatedAuthor = await repository.UpdateAsync(authorEntity);
        return mapper.Map<Author>(updatedAuthor);
    }

    public async Task DeleteAsync(int id)
    {
        await repository.DeleteAsync(id);
    }
}