using DataAccess.Repositories;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;

namespace Domain.Services;

public class UserService(UserRepository repository, IMapper mapper) : ICrudService<User>
{
    public async Task<User?> CreateAsync(User dto)
    {
        var userEntity = mapper.Map<DataAccess.Models.User>(dto);
        var createdUser = await repository.CreateAsync(userEntity);
        return mapper.Map<User>(createdUser);
    }
    
    public async Task<User?> GetByIdAsync(int id)
    {
        var userEntity = await repository.GetByIdAsync(id);
        return mapper.Map<User>(userEntity);
    }
    
    public async Task<User?> GetByEmailAsync(string? email)
    {
        var userEntity = await repository.GetByEmailAsync(email);
        return mapper.Map<User>(userEntity);
    }
    
    public async Task<IEnumerable<User>?> GetAllAsync()
    {
        var userEntities = await repository.GetAllAsync();
        return mapper.Map<IEnumerable<User>>(userEntities);
    }
    
    public async Task<User?> UpdateAsync(User dto)
    {
        var userEntity = mapper.Map<DataAccess.Models.User>(dto);
        var updatedUser = await repository.UpdateAsync(userEntity);
        return mapper.Map<User>(updatedUser);
    }
    
    public async Task DeleteAsync(int id)
    {
        await repository.DeleteAsync(id);
    }
}