using backend.Data;
using backend.Data.Seeding;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("WorldBuilder");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("Connection string 'WorldBuilder' was not found.");
}

Console.WriteLine("WorldBuilder connection string loaded successfully.");

builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(allowIntegerValues: false)));
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<WorldBuilderContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentityCore<IdentityUser>(options => 
    options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<WorldBuilderContext>();

builder.Services.AddScoped<ISeeder, WorldSeeder>();
builder.Services.AddScoped<ISeeder, SettlementSeeder>();
builder.Services.AddScoped<ISeeder, CharacterSeeder>();
builder.Services.AddScoped<ApplicationSeeder>();

// builder.Services.AddAuthentication(options =>
// {
//     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//     options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
// }).AddJwtBearer(options =>
// {
//    
// });

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
