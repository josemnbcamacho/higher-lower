using System.Reflection;
using HigherOrLower.API.Repository;
using HigherOrLower.API.Repository.Interfaces;
using HigherOrLower.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<HigherLowerDbContext>();
builder.Services.AddTransient<IHigherOrLowerGameManager, HigherOrLowerGameManager>();
builder.Services.AddTransient<IHigherLowerGameRepository, HigherLowerGameRepository>();
builder.Services.AddTransient<IDeckRepository, DeckRepository>();
builder.Services.AddTransient<IPlayerRepository, PlayerRepository>();
builder.Services.AddSingleton<IDeckCreator, DeckCreator>();
builder.Services.AddTransient<IPlayerCreator, PlayerCreator>();

builder.Services.AddControllers();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder => builder
            .WithOrigins("http://localhost:3000", "http://localhost:5173")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Configures swagger endpoint documentation
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// (JMBC) Hack to get the migrations to run on app start
// for simplicity, we'll just run the migrations here
// should not be used in a real app
using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;
    var context = services.GetRequiredService<HigherLowerDbContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "HigherOrLower API");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();