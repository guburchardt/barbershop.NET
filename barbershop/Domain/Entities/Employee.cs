namespace barbershop.Domain.Entities;

public class Employee
{
    public Guid Id {get; private set;}
    public string FullName {get; private set;} = null!;
    public bool IsActive {get; private set;}
    public DateTime CreatedAt {get; private set;}
    public DateTime UpdatedAt {get; private set;}

    // Cronstructor protected for ORM (future)
    protected Employee() { }

    public Employee(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Employee name cannot be empty.");
        
        Id = Guid.NewGuid();
        FullName = fullName;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Rename (string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Employee name cannot be empty.");

        if (FullName == fullName)
            return;

        FullName = fullName;
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
