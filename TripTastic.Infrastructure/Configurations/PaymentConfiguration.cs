using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripTastic.Domain.Entities;

namespace TripTastic.Infrastructure.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Amount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(p => p.PaymentMethod)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(p => p.Status)
            .IsRequired()
            .HasMaxLength(20)
            .HasDefaultValue("PAID");

        builder.Property(p => p.PaidAt)
            .IsRequired();

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.HasOne(p => p.Booking)
            .WithOne()
            .HasForeignKey<Payment>(p => p.BookingId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(p => p.BookingId)
            .IsUnique();
    }
}