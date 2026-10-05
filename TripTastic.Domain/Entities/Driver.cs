namespace TripTastic.Domain.Entities;

public class Driver
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string LicenseNumber { get; set; } = string.Empty;

    public DateTime LicenseExpiryDate { get; set; }

    public int ExperienceYears { get; set; }

    public string? ProfilePhoto { get; set; }

    public string Status { get; set; } = "PENDING";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User User { get; set; } = null!;
}