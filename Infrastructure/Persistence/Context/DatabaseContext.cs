using Domain.Entities;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence.Context;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

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
        modelBuilder.Entity<User>().HasData([
            new User("alberto", "salas", "root", Roles.Admin),
            new User("ana", "sanz", "root", Roles.User),
            ]);

        modelBuilder.Entity<Hotel>().HasData([
            new Hotel("boston", "c/las torres", Cities.Zaragoza),
            new Hotel("altamira", "c/los olmos", Cities.Madrid),
            ]);

        // TODO - hotel como clave externa
        modelBuilder.Entity<Room>().HasData([
            new Room(1, RoomTypes.Single),
            new Room(1, RoomTypes.Single),
            new Room(1, RoomTypes.Double),
            new Room(1, RoomTypes.Double),
            ]);

        modelBuilder.Entity<Guest>().HasData([
            new Guest("alfonso", "34455645F"),
            new Guest("maria", "34455645K"),
            ]);

        //base.OnModelCreating(modelBuilder);
    }
}
