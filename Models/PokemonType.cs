using System.ComponentModel.DataAnnotations;

namespace Pokedex.Models
{
    public class PokemonType
    {
        [Key]
        public int id { get; set; }
        public int pokemonId { get; set; }
        public int typeId { get; set; }
    }
}
