using CommandsService.Data;
using CommandsService.Dto;
using CommandsService.Models;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Register Mapster
builder.Services.AddSingleton<IMapper, Mapper>();

// Use InMemory database for Development
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("InMem"));

//Register Repositories for dependency injection
builder.Services.AddScoped<ICommandRepo, CommandRepo>();

// Register the mapping configuration for Mapster
var config = new TypeAdapterConfig();
config.NewConfig<Platform, PlatformReadDto>();  
config.NewConfig<Command, CommandReadDto>();  
config.NewConfig<Command, CommandCreateDto>();  

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

app.Run();
