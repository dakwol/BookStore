using AutoMapper;
using DataAccess.Repositories;
using Domain.Interfaces;
using Domain.Models;

namespace Domain.Services;

public class PublisherService(PublisherRepository repository, IMapper mapper) : ICrudService<Publisher>
{
    public async Task<Publisher?> CreateAsync(Publisher dto)
    {
        var publisherEntity = mapper.Map<DataAccess.Models.Publisher>(dto);
        var createdPublisher = await repository.CreateAsync(publisherEntity);
        return mapper.Map<Publisher>(createdPublisher);
    }

    public async Task<Publisher?> GetByIdAsync(int id)
    {
        var publisherEntity = await repository.GetByIdAsync(id);
        return mapper.Map<Publisher>(publisherEntity);
    }

    public async Task<IEnumerable<Publisher>?> GetAllAsync()
    {
        var publisherEntities = await repository.GetAllAsync();
        return mapper.Map<IEnumerable<Publisher>>(publisherEntities);
    }

    public async Task<Publisher?> UpdateAsync(Publisher dto)
    {
        var publisherEntity = mapper.Map<DataAccess.Models.Publisher>(dto);
        var updatedPublisher = await repository.UpdateAsync(publisherEntity);
        return mapper.Map<Publisher>(updatedPublisher);
    }

    public async Task DeleteAsync(int id)
    {
        await repository.DeleteAsync(id);
    }
}
