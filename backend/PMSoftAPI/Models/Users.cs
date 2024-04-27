using DataAccess.Enums;

namespace PMSoftAPI.Models;

    public class UserCreate
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole? Role { get; set; } = UserRole.User;
    }

    public class UserGet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Role { get; set; }
    }

    public class UserLogin
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
