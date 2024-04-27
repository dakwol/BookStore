namespace PMSoftAPI.Models;

public class PublisherJson
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}

public class PublisherCreate
{
    public string Name { get; set; } = null!;
}

public class PublisherGet
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}

public class PublisherUpdate
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}
