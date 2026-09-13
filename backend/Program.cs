using backend.Data;
using backend.Data.Seeding;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("WorldBuilder");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("Connection string 'WorldBuilder' was not found.");
}

Console.WriteLine("WorldBuilder connection string loaded successfully.");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<WorldBuilderContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddScoped<ISeeder, CharacterSeeder>();
builder.Services.AddScoped<ApplicationSeeder>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<WorldBuilderContext>();
    await context.Database.MigrateAsync();
    await scope.ServiceProvider.GetRequiredService<ApplicationSeeder>()
        .SeedAllAsync(context, scope.ServiceProvider);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
