using HelloWorld.Application.Abstractions;
using HelloWorld.Application.Greetings;
using HelloWorld.Application.Greetings.Commands;
using HelloWorld.Application.Greetings.Queries;
using HelloWorld.DataAccess;
using HelloWorld.Models.Translations;

var builder = WebApplication.CreateBuilder(args);

// CORS – allow the SPA origin on Cloud Run (and localhost for dev)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add controllers from the Service layer assembly
builder.Services.AddControllers()
    .AddApplicationPart(typeof(HelloWorld.Service.Controllers.GreetingsController).Assembly);

// DataAccess registrations (Singleton = in-memory store persists for app lifetime)
builder.Services.AddSingleton<IGreetingRepository, InMemoryGreetingRepository>();
builder.Services.AddSingleton<ITranslationProvider, InMemoryTranslationProvider>();

// Command handlers
builder.Services.AddScoped<ICommandHandler<CreateGreetingCommand, GreetingResponse>, CreateGreetingCommandHandler>();
builder.Services.AddScoped<ICommandHandler<MakeGreetingFormalCommand, GreetingResponse>, MakeGreetingFormalCommandHandler>();
builder.Services.AddScoped<ICommandHandler<MakeGreetingCasualCommand, GreetingResponse>, MakeGreetingCasualCommandHandler>();
builder.Services.AddScoped<ICommandHandler<TranslateGreetingCommand, GreetingResponse>, TranslateGreetingCommandHandler>();

// Query handlers
builder.Services.AddScoped<IQueryHandler<GetAllGreetingsQuery, IReadOnlyList<GreetingResponse>>, GetAllGreetingsQueryHandler>();
builder.Services.AddScoped<IQueryHandler<GetGreetingByIdQuery, GreetingResponse>, GetGreetingByIdQueryHandler>();

builder.Services.AddOpenApi();

var app = builder.Build();

Console.WriteLine(app.Environment.IsDevelopment());
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors();

app.MapControllers();

app.Run();
