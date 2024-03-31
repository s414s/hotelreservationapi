using Application.Contracts;
using Application.Implementations;
using Domain.Contracts;
using Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services
//builder.Services.AddScoped<IAuthService, AuthService>();
//builder.Services.AddScoped<IBookingService, BookingService>();
//builder.Services.AddScoped<IHotelService, HotelService>();
//builder.Services.AddScoped<IRoomService, RoomService>();
//builder.Services.AddScoped<IUserService, UserService>();

// Add repos
//builder.Services.AddScoped<IRepository<User>, UserR>();

// Or if you have a common repository implementation, you can register it as a generic service
//builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
