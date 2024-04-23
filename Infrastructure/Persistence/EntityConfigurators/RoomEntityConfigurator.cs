using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurators;

public class RoomEntityConfigurator : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(h => h.Storey)
            .IsRequired();

        builder.Property(h => h.Type)
            .IsRequired()
            .HasConversion<string>();

        // One-to-many relationship configuration
        builder
            .HasMany(r => r.Bookings) // One room has many bookings
            .WithOne(b => b.Room) // Each booking belongs to one room
            .HasForeignKey(b => b.RoomId) // Foreign key property in Room
            .OnDelete(DeleteBehavior.Cascade) // Cascade delete
            ;
    }
}
