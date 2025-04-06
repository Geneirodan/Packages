using Geneirodan.Abstractions.Mapping;
using Geneirodan.Abstractions.Repositories;
using Geneirodan.EntityFrameworkCore;
using Geneirodan.SampleApi.Domain;
using Geneirodan.SampleApi.Mappers;
using Geneirodan.SampleApi.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DbContext, ApplicationContext>(x => x.UseInMemoryDatabase("Sample"));
builder.Services.AddSingleton<IEntityMapper<DomainEntity, EfEntity>, EntityMapper>();
builder.Services.AddSingleton<IEntityMapper<EfEntity, DomainEntity>, ReverseEntityMapper>();
builder.Services.AddScoped<IRepository<DomainEntity, int>, Repository<DomainEntity, int, EfEntity>>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

public sealed partial class Program;