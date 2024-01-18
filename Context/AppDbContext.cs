using Microsoft.EntityFrameworkCore;
using Pokedex.Models;

namespace Pokedex.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<Gym> Gyms { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Models.Type> Types { get; set; }
    }
}
