using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models;

public class Country
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;
    
    public ICollection<Author>? Authors { get; set; }
}