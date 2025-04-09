using System.Reflection;
using FluentValidation;
using Geneirodan.Abstractions.Mapping;
using Geneirodan.Abstractions.Repositories;
using Geneirodan.EntityFrameworkCore;
using Geneirodan.MediatR;
using Geneirodan.Observability;
using Geneirodan.SampleApi.Domain;
using Geneirodan.SampleApi.Mappers;
using Geneirodan.SampleApi.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DbContext, ApplicationContext>(x => x.UseInMemoryDatabase("Sample"));
builder.Services.AddSingleton<IEntityMapper<DomainEntity, EfEntity>, EntityMapper>();
builder.Services.AddSingleton<IEntityMapper<EfEntity, DomainEntity>, ReverseEntityMapper>();
builder.Services.AddScoped<IRepository<DomainEntity, int>, Repository<DomainEntity, int, EfEntity>>();

builder.Services.AddMediatRPipeline(Assembly.GetExecutingAssembly());
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.AddSerilog();
builder.Services.AddSharedOpenTelemetry(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();