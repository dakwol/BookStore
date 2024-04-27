using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options)
{
    //Database.EnsureDeleted();   // удаляем бд со старой схемой
    //Database.EnsureCreated();   // создаем бд с новой схемой

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Author> Authors { get; set; } = null!;
    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<Country> Countries { get; set; } = null!;
    public DbSet<Genre> Genres { get; set; } = null!;
    public DbSet<Publisher> Publishers { get; set; } = null!;
}