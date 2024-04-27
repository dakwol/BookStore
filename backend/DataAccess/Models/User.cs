using System.ComponentModel.DataAnnotations;
using DataAccess.Enums;

namespace DataAccess.Models;

public class User
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string PasswordHash { get; set; }  = null!;

    [Required] 
    public string Name { get; set; }  = null!;

    [Required]
    public UserRole Role { get; set; }
    
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
}