
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiMovies.Data.Entities;

namespace WebApiMovies.Data.Configuration
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasData(

                new Genre { Id = 1, Name= "Comedy"},
                new Genre { Id = 2, Name = "Horror" },
                new Genre { Id = 3, Name = "Action" },
                new Genre { Id = 4, Name = "Drama" },
                new Genre { Id = 5, Name = "Romance" },
                new Genre { Id = 6, Name = "Adventure" }
            );
        }
    }
}
