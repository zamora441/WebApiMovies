using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiMovies.Data.Entities;

namespace WebApiMovies.Data.Configuration
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(
                new Country { Id= 1, Name="United States"},
                new Country { Id = 2,Name = "Canada" },
                new Country { Id = 3,Name = "Germany" },
                new Country { Id = 4,Name = "Mexico" },
                new Country { Id = 5,Name = "Costa Rica" },
                new Country { Id = 6,Name = "China" },
                new Country { Id = 7,Name = "England" }
            );
        }
    }
}
