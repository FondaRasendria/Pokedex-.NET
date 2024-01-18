using System.ComponentModel.DataAnnotations;

namespace Pokedex.Models
{
    public class Type
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
    }
}
