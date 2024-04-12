using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurators;

public class HotelEntityConfigurator : IEntityTypeConfiguration<Hotel>
{
    public void Configure(EntityTypeBuilder<Hotel> builder)
    {
        builder.HasKey(h => h.Id);

        builder.Property(h => h.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(h => h.Address)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(h => h.City)
            .IsRequired()
            .HasMaxLength(50)
            .HasConversion<string>();

        // One-to-many relationship configuration
        builder
            .HasMany(h => h.Rooms)
            .WithOne(r => r.Hotel)
            .HasForeignKey(b => b.HotelId)
            .OnDelete(DeleteBehavior.Cascade)
            ;
    }
}
