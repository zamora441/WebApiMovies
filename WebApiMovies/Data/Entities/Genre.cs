using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiMovies.Data.Entities.Intermediate_Entities;

namespace WebApiMovies.Data.Entities
{
    public class Genre : BaseEntity
    {
        [Required]
        [StringLength(50)]
        [Column("GenreName")]
        public string Name { get; set; } = null!;
        public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
    }
}
