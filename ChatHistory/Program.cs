using AutoMapper;
using ChatHistory.Api.Core;
using ChatHistory.Api.Mappings;
using ChatHistory.Api.Services;
using ChatHistory.Api.Services.Interfaces;
using ChatHistory.Domain.Events;
using ChatHistory.Domain.Users;
using ChatHistory.Persistence;
using ChatHistory.Persistence.Repositories;
using ChatHistory.Shared.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ErrorHandlingMiddleware>();

ConfigurePersistance(builder.Services);

DbContextGenerator.Initialize(builder.Services);

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

ConfigureRepositories(builder.Services);
ConfigureServices(builder.Services);
ConfigureAutoMapper(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllers();

app.Run();

void ConfigurePersistance(IServiceCollection services)
{
    services.AddDbContext<AppDbContext>
    (ob => ob.UseInMemoryDatabase("ChatHistoryDb"));

    services.AddScoped<IUnitOfWork, UnitOfWork>();
}

void ConfigureRepositories(IServiceCollection services)
{
    services.AddScoped<IEventRepository, EventRepository>();
}

void ConfigureServices(IServiceCollection services)
{
    services.AddScoped<IFormatDataService, FormatDataService>();
}

void ConfigureAutoMapper(IServiceCollection services)
{
    var mappingConfig = new MapperConfiguration(mc =>
    {
        mc.AddProfile(new EventMapping());

    });

    IMapper mapper = mappingConfig.CreateMapper();
    services.AddSingleton(mapper);
}
