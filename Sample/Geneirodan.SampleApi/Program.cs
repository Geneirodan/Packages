using System.Reflection;
using FluentValidation;
using Geneirodan.Mediator.Behaviors;
using Geneirodan.Observability;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediator(options =>
{
    options.ServiceLifetime = ServiceLifetime.Scoped;
    options.PipelineBehaviors =
    [
        typeof(AuthorizationBehavior<,>),
        typeof(LoggingPreProcessor<,>),
        typeof(UnhandledExceptionBehavior<,>),
        typeof(ValidationBehavior<,>),
    ];
});
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.AddSerilog();
builder.Services.AddSharedOpenTelemetry(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
