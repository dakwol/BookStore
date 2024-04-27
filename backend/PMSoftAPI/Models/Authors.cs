namespace PMSoftAPI.Models;

public class AuthorCreate
{
    public string Name { get; set; } = null!;
    public int? CountryId { get; set; }
}

public class AuthorUpdate
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int? CountryId { get; set; }
}

public class AuthorGet
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public CountryGet? Country { get; set; }
}