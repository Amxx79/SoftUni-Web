using CinemaApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static CinemaApp.Common.EntityValidationConstants.Movie;
using static CinemaApp.Common.ApplicationConstants;

namespace CinemaApp.Data.Configuration
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(m => m.Id);

            builder
                .Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(TitleMaxLength);

            builder
                .Property(m => m.Genre)
                .IsRequired()
                .HasMaxLength(GenreMaxLength);

            builder
                .Property(m => m.Director)
                .IsRequired()
                .HasMaxLength(DirectorNameMaxLength);

            builder
                .Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(DescriptionMaxLength);

            builder.HasData(this.SeedMovies());

            builder
                .Property(m => m.ImageUrl)
                .IsRequired(false)
                .HasMaxLength(ImageUrlMaxLength)
                .HasDefaultValue(NoImageUrl);
        }

        private List<Movie> SeedMovies()
        {
            List<Movie> movies = new List<Movie>()
            {
                new Movie()
                {
                    Title = "A Twist of the Wrist",
                    Genre = "Informative",
                    ReleaseDate = new DateTime(2009, 10, 01),
                    Director = "Cheef",
                    Duration = 110,
                    Description = "If you wanna be a better rider , you should watch multiple times and practice... There is no cheap advices in Twist of the Wrist ; so scientific and practicable.",
                },
                new Movie()
                {
                    Title = "Born to Race",
                    Genre = "Sport",
                    ReleaseDate = new DateTime(2014, 09, 09),
                    Director = "Alex Ranarivelo",
                    Duration = 97,
                    Description = "When two small-town rival racers are forced to work together to defeat a reigning champion, it becomes survival of the fastest in an ultimate race to the finish line. Stars Brett Davern.",
                }
            };

            return movies;
        }
    }
}
