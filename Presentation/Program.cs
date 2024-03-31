using Application.Contracts;
using Application.Implementations;
using Domain.Contracts;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;

// config info
// https://medium.com/@saisiva249/how-to-configure-postgres-database-for-a-net-a2ee38f29372

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

var Configuration = builder.Configuration;
builder.Services.AddDbContext<DatabaseContext>(options =>
        options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase")));

// Or if you have a common repository implementation, you can register it as a generic service
//builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

var app = builder.Build();

// ===================================

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
