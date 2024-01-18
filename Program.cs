using Microsoft.EntityFrameworkCore;
using Pokedex.Context;
using Pokedex.Repositories;

var builder = WebApplication.CreateBuilder(args);

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("String koneksi DB 'Main' tidak ditemukan / invalid. Cek file appsettings.json.");

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<GymRepository>();
builder.Services.AddScoped<RegionRepository>();
builder.Services.AddScoped<TypeRepository>();
builder.Services.AddScoped<PokemonRepository>();
builder.Services.AddScoped<TrainerRepository>();
builder.Services.AddScoped<PokemonTrainerRepository>();
builder.Services.AddScoped<PokemonTypeRepository>();

var allOrigins = "allowOrigins";
builder.Services.AddCors(opt => opt.AddPolicy(allOrigins, policy =>
{
    policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(allOrigins);
app.UseAuthorization();

app.MapControllers();

app.Run();
