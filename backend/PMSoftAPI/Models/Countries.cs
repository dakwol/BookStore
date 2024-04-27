namespace PMSoftAPI.Models;

public class CountryCreate
{
    public string Name { get; set; } = null!;
}

public class CountryGet
{
    public int? Id { get; set; }
    public string Name { get; set; } = null!;
}

public class CountryUpdate
{
    public int? Id { get; set; }
    public string Name { get; set; } = null!;
}