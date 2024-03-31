using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurators;

public class BookingEntityConfigurator : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Start)
            .IsRequired();

        builder.Property(b => b.End)
            .IsRequired();

        // One-to-many relationship configuration
        builder
            .HasMany(r => r.Guests) // One booking has many guests
            .WithOne(b => b.Booking) // Each guest belongs to one room
            .HasForeignKey(b => b.BookingId) // Foreign key property in Booking
            .OnDelete(DeleteBehavior.Cascade) // Cascade delete
            ;
    }
}
