using Application.Contracts;
using Application.Identity;
using Application.Implementations;
using Domain.Contracts;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

Console.WriteLine("Building the app");

// config info https://medium.com/@saisiva249/how-to-configure-postgres-database-for-a-net-a2ee38f29372
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("allOrigins", policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin();
        policyBuilder.AllowAnyHeader();
        policyBuilder.AllowAnyMethod();
    });
});

// Add authentication and authorization
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
builder.Services
    .AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        // TODO READ https://matteosonoio.it/aspnet-core-authentication-schemes/
        // TOOD READ https://learn.microsoft.com/es-es/aspnet/core/security/authentication/jwt-authn?view=aspnetcore-8.0&tabs=windows

        // IDENTITY PROVIDER
        options.RequireHttpsMetadata = false; // make it true for poduction
        options.Authority = builder.Configuration["JWT:Issuer"];
        options.ClaimsIssuer = builder.Configuration["JWT:Issuer"];

        // The target application for which the JWT is emitted
        options.Audience = builder.Configuration["JWT:Audience"];

        var Key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!);
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false, // on production make it true
            ValidateAudience = false, // on production make it true
            ValidateLifetime = false, // on production make it true
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Key),
            //ClockSkew = TimeSpan.Zero,
            ClockSkew = TimeSpan.FromMinutes(5),
        };

    });

// Allows configuring authorization in ASP.NET Core by using the Authorize attribute
//builder.Services.AddAuthorization();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
    {
        policy.RequireRole("Admin");
    });
});

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    {
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header required"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                },
                new string[] {}
            }
        });
    }
);

// Add services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IUserProfile, UserProfile>();

// Add repos
builder.Services.AddScoped<IRepository<Booking>, BookingsRepository>();
builder.Services.AddScoped<IRepository<Guest>, GuestsRepository>();
builder.Services.AddScoped<IRepository<Hotel>, HotelsRepository>();
builder.Services.AddScoped<IRepository<Room>, RoomsRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();

var isDocker = false;
bool.TryParse(Environment.GetEnvironmentVariable("IS_DOCKER"), out isDocker);

builder.Services.AddDbContext<DatabaseContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString(isDocker ? "WebApiDatabase" : "LocalWebApiDatabase")));

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Apply migrations
using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetService<DatabaseContext>();
    if (dbContext == null) Console.WriteLine("Unable to establish connection to db");
    dbContext?.Database.Migrate();
}

// Configure the HTTP request pipeline.

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("allOrigins");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
