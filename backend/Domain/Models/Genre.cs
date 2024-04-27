namespace Domain.Models;

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<Book>? Books { get; set; }
}