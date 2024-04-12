using Domain.Entities;
using Domain.Enum;
using Infrastructure.Persistence.EntityConfigurators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence.Context;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    //    se inyecta la configuración para poder leer el connection string desde ella
    //    protected readonly IConfiguration Configuration;
    //    public DatabaseContext(IConfiguration configuration)
    //    {
    //        Configuration = configuration;
    //    }

    //    protected override void OnConfiguring(DbContextOptionsBuilder options)
    //    {
    //        // connect to postgres with connection string from app settings
    //        options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));

    //        //base.OnConfiguring(options);
    //    }

    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Guest> Guests { get; set; }
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BookingEntityConfigurator());
        modelBuilder.ApplyConfiguration(new GuestEntityConfigurator());
        modelBuilder.ApplyConfiguration(new HotelEntityConfigurator());
        modelBuilder.ApplyConfiguration(new RoomEntityConfigurator());
        modelBuilder.ApplyConfiguration(new UserEntityConfigurator());
        
        // TODO hashear contraseñas
        modelBuilder.Entity<User>().HasData([
            new User{ Id = 1, Name = "alberto", Surname = "salas", Password = "root", Role = Roles.Admin },
            new User{ Id = 2, Name = "ana", Surname = "sanz", Password = "root", Role = Roles.Admin },
            ]);

        modelBuilder.Entity<Hotel>().HasData([
            new Hotel{Id = 1, Name = "boston", Address = "c/las torres", City = Cities.Zaragoza},
            new Hotel{Id = 2, Name = "altamira", Address = "c/los olmos", City = Cities.Madrid},
            ]);

        modelBuilder.Entity<Room>().HasData([
            new Room{ Id = 1, Storey = 1, Type = RoomTypes.Single, HotelId = 1 },
            new Room{ Id = 2, Storey = 1, Type = RoomTypes.Single, HotelId = 1 },
            new Room{ Id = 3, Storey = 1, Type = RoomTypes.Double, HotelId = 1 },
            new Room{ Id = 4, Storey = 1, Type = RoomTypes.Double, HotelId = 1 },

            new Room{ Id = 5, Storey = 1, Type = RoomTypes.Single, HotelId = 2 },
            new Room{ Id = 6, Storey = 1, Type = RoomTypes.Single, HotelId = 2 },
            new Room{ Id = 7, Storey = 1, Type = RoomTypes.Double, HotelId = 2 },
            new Room{ Id = 8, Storey = 1, Type = RoomTypes.Suite, HotelId = 2 },
            ]);

        //base.OnModelCreating(modelBuilder);
    }
}
