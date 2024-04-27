using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models;

public class Book
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = null!;

    public int Year { get; set; }

    public string? Description { get; set; }

    public int? GenreId { get; set; }
    public Genre? Genre { get; set; }
    
    public int? AuthorId { get; set; }
    public Author? Author { get; set; }
    
    public int? PublisherId { get; set; }
    public Publisher? Publisher { get; set; }
    
    [Required]
    [Range(1, 5)]
    public int Rating { get; set; }

    public string? ImagePreview { get; set; }
}