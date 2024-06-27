using System.ComponentModel.DataAnnotations;

namespace WebApiMovies.DTOs.ActorDTOs
{
    public class ActorUpdateDto
    {
        private const string _noSpecialCharactersMessage = "No special characters or numbers are allowed, just letters.";
        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-ZÁ-ÿ ]+$", ErrorMessage = _noSpecialCharactersMessage)]
        public string Name { get; set; } = null!;
        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-ZÁ-ÿ ]+$", ErrorMessage = _noSpecialCharactersMessage)]
        public string LastName { get; set; } = null!;
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [Required]
        public int CountryId { get; set; }
    }
}
