using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripTastic.Domain.Entities;

namespace TripTastic.Infrastructure.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("Bookings");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.PickupLocation)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(b => b.DropLocation)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(b => b.Status)
            .IsRequired()
            .HasMaxLength(20)
            .HasDefaultValue("PENDING");

        builder.Property(b => b.NumberOfPassengers)
            .IsRequired();

        builder.Property(b => b.TravelDate)
            .IsRequired();

        builder.Property(b => b.CreatedAt)
            .IsRequired();

        // Customer → Bookings
        builder.HasOne(b => b.Customer)
            .WithMany()
            .HasForeignKey(b => b.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        // Driver → Bookings
        builder.HasOne(b => b.Driver)
            .WithMany()
            .HasForeignKey(b => b.DriverId)
            .OnDelete(DeleteBehavior.Restrict);

        // Vehicle → Bookings
        builder.HasOne(b => b.Vehicle)
            .WithMany()
            .HasForeignKey(b => b.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}