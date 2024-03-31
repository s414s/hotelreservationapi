using Domain.Entities;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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
        // TODO hashear contraseñas
        modelBuilder.Entity<User>().HasData([
            new User("alberto", "salas", "root", Roles.Admin),
            new User("ana", "sanz", "root", Roles.User),
            ]);

        modelBuilder.Entity<Hotel>().HasData([
            new Hotel{Id = 1, Name = "boston", Address = "c/las torres", City = Cities.Zaragoza, Rooms = [
                    new Room(1, RoomTypes.Single),
                    new Room(1, RoomTypes.Single),
                    new Room(1, RoomTypes.Double),
                    new Room(1, RoomTypes.Double),
                ]},
            new Hotel{Id = 2, Name = "altamira", Address = "c/los olmos", City = Cities.Madrid, Rooms = [
                    new Room(1, RoomTypes.Single),
                    new Room(1, RoomTypes.Single),
                    new Room(1, RoomTypes.Double),
                    new Room(1, RoomTypes.Double),
                ]},
            ]);

        //// TODO - hotel como clave externa
        //modelBuilder.Entity<Room>().HasData([
        //    new Room(1, RoomTypes.Single),
        //    new Room(1, RoomTypes.Single),
        //    new Room(1, RoomTypes.Double),
        //    new Room(1, RoomTypes.Double),

        //    new Room(1, RoomTypes.Single),
        //    new Room(1, RoomTypes.Double),
        //    new Room(1, RoomTypes.Suite),
        //    new Room(1, RoomTypes.Suite),
        //    ]);

        modelBuilder.Entity<Guest>().HasData([
            new Guest("alfonso", "34455645F"),
            new Guest("maria", "34455645K"),
            ]);

        //base.OnModelCreating(modelBuilder);
    }
}
