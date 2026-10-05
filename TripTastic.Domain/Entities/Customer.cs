namespace TripTastic.Domain.Entities;

public class Customer
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string? ProfilePhoto { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User User { get; set; } = null!;
}