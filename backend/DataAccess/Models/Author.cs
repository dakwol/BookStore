using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models;

public class Author
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public int? CountryId { get; set; }
    public Country? Country { get; set; }

    public ICollection<Book>? Books { get; set; }
}