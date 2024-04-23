using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.EntityConfigurators;

public class GuestEntityConfigurator : IEntityTypeConfiguration<Guest>
{
    public void Configure(EntityTypeBuilder<Guest> builder)
    {
        builder.HasKey(g => g.Id);

        builder.Property(g => g.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(g => g.DNI)
            .IsRequired()
            .HasMaxLength(9);

        // One-to-one relationship
        builder.HasOne(g => g.Booking)
               .WithMany(b => b.Guests)
               .HasForeignKey(g => g.BookingId);
    }
}
