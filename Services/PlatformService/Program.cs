using PlatformService.Data;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore;
using Mapster;
using MapsterMapper;
using PlatformService.DTOs;
using PlatformService.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add datbase context
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));

//Register Repositories for dependency injection
builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();

// Register Mapster
builder.Services.AddSingleton<IMapper, Mapper>();

// Register the mapping configuration for Mapster
var config = new TypeAdapterConfig();
config.NewConfig<Platform, PlatformReadDto>();  // Mapping between Platform model and PlatformReadDto
builder.Services.AddSingleton(config);  // Add the configuration to DI

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

PrepDb.prepPopulation(app);

app.Run();
