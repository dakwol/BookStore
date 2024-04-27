namespace PMSoftAPI.Models;

public class BookCreate
{
    public string Title { get; set; } = null!;
    public int Year { get; set; }
    public string? Description { get; set; }
    public int? GenreId { get; set; }
    public int? AuthorId { get; set; }
    public int? PublisherId { get; set; }
    public int Rating { get; set; }
    public string? ImagePreview { get; set; }
}

public class BookGet
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public int Year { get; set; }
    public string? Description { get; set; }
    public GenreGet? Genre { get; set; }
    public AuthorGet? Author { get; set; }
    public PublisherGet? Publisher { get; set; }
    public int Rating { get; set; }
    public string? ImagePreview { get; set; }
}

public class BookUpdate
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public int Year { get; set; }
    public string? Description { get; set; }
    public int? GenreId { get; set; }
    public int? AuthorId { get; set; }
    public int? PublisherId { get; set; }
    public int Rating { get; set; }
    public string? ImagePreview { get; set; }
}
