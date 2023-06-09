using HouseReservationWebAPI;
using HouseReservationWebAPI.Data;
using HouseReservationWebAPI.Repository;
using HouseReservationWebAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File("log/house_reservation_logs.txt", rollingInterval: RollingInterval.Month)
    .CreateLogger();
builder.Host.UseSerilog();

// Repository 
builder.Services.AddScoped<IHouseRepository, HouseRepository>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingConfiguration));

// Controllers 
builder.Services.AddControllers(option =>
    {
       // option.ReturnHttpNotAcceptable = true;
    })
    .AddNewtonsoftJson();

// EF Core
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLServerConnection"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();