using System.ComponentModel.DataAnnotations;

namespace Pokedex.Models
{
    public class PokemonTrainer
    {
        [Key]
        public int id { get; set; }
        public int pokemonId { get; set; }
        public int trainerId { get; set; }
    }
}
