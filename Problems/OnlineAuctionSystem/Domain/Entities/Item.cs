namespace OAS.Domain.Entities;

public class Item
{
    public Guid Id { get; }
    public string Name { get; }
    public string Description { get; }
    public string Category { get; }

    public Item(Guid id, string name, string description, string category)
    {
        Id = id;
        Name = name;
        Description = description;
        Category = category;
    }
}