using System.ComponentModel.DataAnnotations;
using WebApiMovies.Data.Entities.Intermediate_Entities;

namespace WebApiMovies.Data.Entities
{
    public class Actor : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;
        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = null!;
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", Name, LastName);
            }
        }
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public int Age
        {
            get
            {
                DateTime currentDate = DateTime.Now;
                int age = currentDate.Year - BirthDate.Year;
                if(currentDate < BirthDate.AddYears(age))
                {
                    age--;
                }
                return age;
            }
        }
        public string? ImageUrl { get; set; }
        public string? ImageId { get; set; }

        [Required]
        public int CountryId { get; set; }
        public Country Country { get; set; } = null!;
        public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
    }
}
