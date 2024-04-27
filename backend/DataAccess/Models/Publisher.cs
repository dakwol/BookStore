using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models;

public class Publisher
{
    [Required]
    public int Id { get; set; }

    [Required] 
    public string Name { get; set; } = null!;
    
    public ICollection<Book>? Books { get; set; }
}