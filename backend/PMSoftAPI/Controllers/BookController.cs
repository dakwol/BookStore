using AutoMapper;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMSoftAPI.Models;

namespace PMSoftAPI.Controllers;

[Authorize]
[ApiController]
[Route("books")]
public class BookController(BookService bookService, IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<BookGet>> CreateBook(BookCreate bookCreate)
    {
        try
        {
            var book = mapper.Map<Book>(bookCreate);
            var createdBook = await bookService.CreateAsync(book);
            var bookGet = mapper.Map<BookGet>(createdBook);
            return CreatedAtAction(nameof(GetBook), new { id = bookGet.Id }, bookGet);
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to create book: {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<BookGet>> GetBook(int id)
    {
        try
        {
            var book = await bookService.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound("Book not found.");
            }
            var bookGet = mapper.Map<BookGet>(book);
            return Ok(bookGet);
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to get book: {ex.Message}");
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookGet>>> GetAllBooks(int? genreId)
    {
        try
        {
            var books = await bookService.GetAllAsync(genreId);
            var booksGet = mapper.Map<IEnumerable<BookGet>>(books);
            return Ok(booksGet);
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to get all books: {ex.Message}");
        }
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateBook(int id, BookUpdate bookUpdate)
    {
        try
        {
            var book = mapper.Map<Book>(bookUpdate);
            var updatedBook = await bookService.UpdateAsync(book);
            if (updatedBook == null)
            {
                return NotFound("Book not found.");
            }
            return Ok(updatedBook);
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to update book: {ex.Message}");
        }
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        try
        {
            await bookService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to delete book: {ex.Message}");
        }
    }
}

