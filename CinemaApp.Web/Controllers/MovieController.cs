namespace CinemaApp.Web.Controllers
{
    using System.Globalization;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using CinemaApp.Data;
    using CinemaApp.Data.Models;
    using CinemaApp.Web.ViewModels.Movie;
    using CinemaApp.Web.ViewModels.Cinema;

    using static CinemaApp.Common.EntityValidationConstants.Movie;

    public class MovieController : BaseController
    {
        private CinemaDbContext dbContext;

        public MovieController(CinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Movie> allMovies = await this.dbContext
                .Movies
                .ToArrayAsync();

            return View(allMovies);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddMovieInputModel inputModel) 
        {
            bool isReleasedDateValid = DateTime.TryParseExact(inputModel.ReleaseDate, ReleaseDateFormat,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime releaseDate);

            if (!isReleasedDateValid)
            {
                this.ModelState.AddModelError(nameof(inputModel.ReleaseDate)
                    , String.Format("The Released Date must be in the following format: {0}", ReleaseDateFormat));
                return this.View(inputModel);
            }

            if (!this.ModelState.IsValid)
            {
                //If the format of movie isn't good, the form will populate the same form with same data + model errors.
                return this.View(inputModel);
            }

            Movie movie = new Movie()
            {
                Title = inputModel.Title,
                Genre = inputModel.Genre,
                Director = inputModel.Director,
                ReleaseDate = releaseDate,
                Duration = inputModel.Duration,
                Description = inputModel.Description,
                ImageUrl = inputModel.ImageUrl,
            };

            await this.dbContext.Movies.AddAsync(movie);
            await this.dbContext.SaveChangesAsync();

            return this.RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id) 
        {
            Guid movieGuid = Guid.Empty;

            bool isIdValid = this.IsGuidValid(id, ref movieGuid);
            if (!isIdValid)
            {
                return this.RedirectToAction(nameof(Index));
            }

            Movie? movie = await this.dbContext
                .Movies
                .FirstOrDefaultAsync(m => m.Id == movieGuid);

            if (movie == null)
            {
                return this.RedirectToAction(nameof(Index));
            }

            return this.View(movie);
        }

        [HttpGet]
        public async Task<IActionResult> AddToProgram(string? id)
        {
            Guid movieGuid = Guid.Empty;
            bool isGuidValid = this.IsGuidValid(id, ref movieGuid);

            if (!isGuidValid)
            {
                return this.RedirectToAction(nameof(Index));
            }

            Movie? movie = await this.dbContext
                .Movies
                .FirstOrDefaultAsync(m => m.Id == movieGuid);

            if (movie == null) 
            {
                return this.RedirectToAction(nameof(Index));
            }

            AddMovieToCinemaInputModel viewModel = new AddMovieToCinemaInputModel()
            {
                Id = id!,
                Title = movie.Title,
                Cinemas = await this.dbContext
                    .Cinemas
                    .Include(c => c.CinemaMovies)
                    .ThenInclude(cm => cm.Movie)
                    .Select(c => new CinemaCheckBoxInputModel()
                    {
                        Id = c.Id.ToString(),
                        Name = c.Name,
                        Location = c.Location,
                        IsSelected = c.CinemaMovies
                            .Any(cm => cm.Movie.Id == movieGuid)
                    })
                    .ToArrayAsync()

            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddToProgram(AddMovieToCinemaInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            Guid movieGuid = Guid.Empty;
            bool isGuidValid = this.IsGuidValid(model.Id, ref movieGuid);

            if (!isGuidValid)
            {
                return this.RedirectToAction(nameof(Index));
            }

            Movie? movie = await this.dbContext
                .Movies
                .FirstOrDefaultAsync(m => m.Id == movieGuid);

            if (movie == null) 
            {
                return this.RedirectToAction(nameof(Index));
            }

            ICollection<CinemaMovie> entitiesToAdd = new List<CinemaMovie>();

            foreach (var cinemaInputModel in model.Cinemas)
            {
                Guid cinemaGuid = Guid.Empty;
                bool isCinemaGuidValid = this.IsGuidValid(cinemaInputModel.Id, ref cinemaGuid);

                if (!isCinemaGuidValid)
                {
                    this.ModelState.AddModelError(string.Empty, "Invalid cinema selected!");
                    return this.View(model);
                }

                Cinema? cinema = await this.dbContext
                    .Cinemas
                    .FirstOrDefaultAsync(c => c.Id == cinemaGuid);

                if (cinema == null)
                {
                    this.ModelState.AddModelError(string.Empty, "Invalid cinema selected!");
                    return this.View(model);
                }


                CinemaMovie? cinemaMovie = await this.dbContext
                    .CinemasMovies
                    .FirstOrDefaultAsync(cm => cm.CinemaId == cinemaGuid &&
                                        cm.MovieId == movieGuid);

                if (cinemaInputModel.IsSelected)
                {
                    if (cinemaMovie == null)
                    {
                        entitiesToAdd.Add(new CinemaMovie()
                        {
                            Cinema = cinema,
                            Movie = movie,
                        });
                    }

                    else
                    {
                        cinemaMovie.IsDeleted = false;
                    }
                }

                else
                {
                    if (cinemaMovie != null) 
                    {
                        cinemaMovie.IsDeleted = true;
                    }
                }

                await this.dbContext.SaveChangesAsync();

            }

            await this.dbContext.CinemasMovies.AddRangeAsync(entitiesToAdd);
            await this.dbContext.SaveChangesAsync();

            return this.RedirectToAction(nameof(Index), "Cinema");
        }
    }
}
