using System.ComponentModel.DataAnnotations;

namespace Pokedex.Models
{
    public class Region
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string image { get; set;}
    }
}
