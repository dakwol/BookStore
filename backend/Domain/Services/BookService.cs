using AutoMapper;
using DataAccess.Repositories;
using Domain.Interfaces;
using Domain.Models;

namespace Domain.Services;

public class BookService(BookRepository repository, IMapper mapper) : ICrudService<Book>
{
    public async Task<Book?> CreateAsync(Book dto)
    {
        var bookEntity = mapper.Map<DataAccess.Models.Book>(dto);
        var createdBook = await repository.CreateAsync(bookEntity);
        return mapper.Map<Book>(createdBook);
    }

    public async Task<Book?> GetByIdAsync(int id)
    {
        var bookEntity = await repository.GetByIdAsync(id);
        return mapper.Map<Book>(bookEntity);
    }

    public async Task<IEnumerable<Book>?> GetAllAsync(int? genreId)
    {
        var bookEntities = await repository.GetAllAsync();
        if (!genreId.HasValue) return mapper.Map<IEnumerable<Book>>(bookEntities);
        if (bookEntities != null) bookEntities = bookEntities.Where(b => b.GenreId == genreId);
        return mapper.Map<IEnumerable<Book>>(bookEntities);
    }

    public async Task<Book?> UpdateAsync(Book dto)
    {
        var bookEntity = mapper.Map<DataAccess.Models.Book>(dto);
        var updatedBook = await repository.UpdateAsync(bookEntity);
        return mapper.Map<Book>(updatedBook);
    }

    public async Task DeleteAsync(int id)
    {
        await repository.DeleteAsync(id);
    }
}
