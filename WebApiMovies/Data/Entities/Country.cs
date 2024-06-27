using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiMovies.Data.Entities
{
    public class Country : BaseEntity
    {
        [Required]
        [StringLength(50)]
        [Column("CountryName")]
        public string Name { get; set; } = null!;
        public ICollection<Actor> Actors { get; set; } = new List<Actor>();
    }
}
