using System.ComponentModel.DataAnnotations;

namespace Pokedex.Models
{
    public class Trainer
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public int regionId { get; set; }
        public int gymId { get; set; }
        public string image { get; set; }
        public string descrption { get; set; }
    }
}
