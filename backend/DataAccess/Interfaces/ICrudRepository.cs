namespace DataAccess.Interfaces;

internal interface ICrudRepository<TDalModel> where TDalModel : class
{
    Task<TDalModel?> CreateAsync(TDalModel entity);
    Task<TDalModel?> GetByIdAsync(int? id);
    Task<IEnumerable<TDalModel>?> GetAllAsync();
    Task<TDalModel?> UpdateAsync(TDalModel entity);
    Task DeleteAsync(int id);
}