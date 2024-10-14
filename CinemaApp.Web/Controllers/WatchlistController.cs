using CinemaApp.Data;
using CinemaApp.Data.Models;
using CinemaApp.Web.ViewModels.Watchlist;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static CinemaApp.Common.EntityValidationConstants.Movie;

namespace CinemaApp.Web.Controllers
{
    public class WatchlistController : BaseController
    {
        private readonly CinemaDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public WatchlistController(CinemaDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            string? userId = this.userManager.GetUserId(User);

            IEnumerable<ApplicationUserWatchlistViewModel> watchList = await this.dbContext
                .UsersMovies
                .Include(um => um.Movie)
                .Where(um => um.ApplicationUserId.ToString().ToLower() == userId.ToLower())
                .Select(um => new ApplicationUserWatchlistViewModel()
                {
                    MovieId = um.MovieId.ToString(),
                    Title = um.Movie.Title,
                    Genre = um.Movie.Genre,
                    ReleaseDate = um.Movie.ReleaseDate.ToString(ReleaseDateFormat),
                    ImageUrl = um.Movie.ImageUrl,
                })
                .ToListAsync();

            return View(watchList);
        }
    }
}
