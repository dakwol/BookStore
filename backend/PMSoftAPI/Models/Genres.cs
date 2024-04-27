namespace PMSoftAPI.Models;

public class GenreCreate
{
    public string Name { get; set; } = null!;
}

public class GenreGet
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class GenreUpdate
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}
