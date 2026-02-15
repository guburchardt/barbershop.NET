namespace barbershop.Domain.Entities;

public class Client
{
    public Guid Id { get; private set; }
    public string FullName { get; private set; } = null!;
    public string? Phone { get; private set; }
    public string? Email { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    // ORM Future
    protected Client() {}

    public Client(string fullName,
                    string? phone = null,
                    string? email = null)
    {
        Id = Guid.NewGuid();
        FullName = fullName;
        Phone = phone;
        Email = email;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateContact(string? phone, string? email)
    {
        Phone = phone;
        Email = email;
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

    public void Rename (string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Client name cannot be empty");

        FullName = fullName;
        UpdatedAt = DateTime.UtcNow;
    }

}
