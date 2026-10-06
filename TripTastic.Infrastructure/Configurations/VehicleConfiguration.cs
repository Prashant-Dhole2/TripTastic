using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripTastic.Domain.Entities;

namespace TripTastic.Infrastructure.Configurations;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("Vehicles");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.VehicleNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasIndex(x => x.VehicleNumber)
            .IsUnique();

        builder.Property(x => x.VehicleType)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Brand)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Model)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.ManufacturingYear)
            .IsRequired();

        builder.Property(x => x.Color)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.SeatingCapacity)
            .IsRequired();

        builder.Property(x => x.IsAC)
            .IsRequired();

        builder.Property(x => x.VehiclePhoto)
            .HasMaxLength(500);

        builder.Property(x => x.Status)
            .IsRequired()
            .HasMaxLength(20)
            .HasDefaultValue("PENDING");

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasOne(v => v.Driver)
      .WithMany(d => d.Vehicles)
      .HasForeignKey(v => v.DriverId)
      .OnDelete(DeleteBehavior.Cascade);
    }
}