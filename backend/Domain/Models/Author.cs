namespace Domain.Models;

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int? CountryId { get; set; }
    public Country? Country { get; set; }
    public List<Book>? Books { get; set; }
}