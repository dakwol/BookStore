namespace Domain.Interfaces;

internal interface ICrudService<TDtoModel> where TDtoModel : class
{
    Task<TDtoModel?> CreateAsync(TDtoModel dto);
    Task<TDtoModel?> GetByIdAsync(int id);
    Task<TDtoModel?> UpdateAsync(TDtoModel dto);
    Task DeleteAsync(int id);
}