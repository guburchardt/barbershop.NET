namespace barbershop.Domain.Entities;

public class Service
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }
    public decimal Price { get; private set; }
    public int DurationInMinutes { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    protected Service() { }

    public Service(string name, string? description, decimal price, int durationInMinutes)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name of the service cannot be empity");

        if (price < 0)
            throw new ArgumentException("Price cannot be empty");

        if (durationInMinutes <= 0)
            throw new ArgumentException("Duration have to be higher then zero minutes");

        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Price = price;
        DurationInMinutes = durationInMinutes;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }


    public void UpdateDetails(string name, string? description, decimal price, int durationInMinutes)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name of the service cannot be empty");
        if (price < 0)
            throw new ArgumentException("Price cannot be empty");
        if (durationInMinutes <= 0)
            throw new ArgumentException("Duration have to be higher then zero minutes");
        Name = name;
        Description = description;
        Price = price;
        DurationInMinutes = durationInMinutes;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        if (!IsActive) return;

        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        if (IsActive) return;

        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }

}
