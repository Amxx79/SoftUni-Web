namespace CinemaApp.Web.ViewModels.Movie
{
    using CinemaApp.Web.ViewModels.Cinema;
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.Movie;

    public class AddMovieToCinemaInputModel
    {
        [Required]
        public string Id { get; set; } = null!;

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;
        public IList<CinemaCheckBoxInputModel> Cinemas { get; set; }
            = new List<CinemaCheckBoxInputModel>();

    }
}
