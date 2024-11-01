using GameZone.Data;
using GameZone.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;
using System.Security.Principal;
using static GameZone.Common.ApplicationConstants;

namespace GameZone.Controllers
{
    public class GameController : Controller
    {
        private readonly GameZoneDbContext context;

        public GameController(GameZoneDbContext _dbContext)
        {
            context = _dbContext;
        }

        public async Task<IActionResult> All()
        {
            var model = await this.context
                .Games
                .Select(g => new GameViewModel()
                {
                    Id = g.Id,
                    Title = g.Title,
                    Genre = g.Genre.Name,
                    ReleasedOn = g.ReleasedOn.ToString(DateTimeFormat),
                    ImageUrl = g.ImageUrl,
                    Publisher = g.Publisher.UserName
                })
                .ToListAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddGameInputModel();
            model.Genres = await GetGenres();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddGameInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            model.Genres = await GetGenres();

            DateTime correctDateTime;


            bool isDateTimeCorrect = DateTime.TryParseExact
                (model.ReleasedOn, DateTimeFormat, CultureInfo.CurrentCulture, DateTimeStyles.None, out correctDateTime);

            if (!isDateTimeCorrect)
            {
                return this.View(model);
            }

            Game game = new Game()
            {
                Title = model.Title,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                ReleasedOn = correctDateTime,
                GenreId = model.GenreId,
                PublisherId = User.FindFirstValue(ClaimTypes.NameIdentifier),
            };

            await this.context.Games.AddAsync(game);
            await this.context.SaveChangesAsync();

            return this.RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> MyZone()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);

            //TODO: Check the action
            var model = await this.context
                .Games
                .Include(g => g.GamersGames)
                .Where(g => g.GamersGames.Any(g => g.GamerId == user))
                .Select(g => new GameViewModel()
                {
                    Id = g.Id,
                    Title = g.Title,
                    Genre = g.Genre.Name,
                    ReleasedOn = g.ReleasedOn.ToString(DateTimeFormat),
                    ImageUrl = g.ImageUrl,
                    Publisher = g.Publisher.UserName
                })
                .ToListAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddToMyZone(int id)
        {
            Game? game = await this.context
                .Games
                .Where(g => g.Id == id)
                .Include(g => g.GamersGames)
                .FirstOrDefaultAsync();

            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (context.GamersGames.Any(gg => gg.GamerId == user && gg.GameId == game.Id))
            {
                return View(nameof(All));
            }

            GamerGame newGamerGame = new GamerGame()
            {
                GamerId = user,
                GameId = game.Id,
            };

            await context.GamersGames.AddAsync(newGamerGame);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(MyZone));
        }

        [HttpGet]
        public async Task<IActionResult> StrikeOut(int id)
        {
            Game? game = await this.context
                .Games
                .Where(g => g.Id == id)
                .Include(g => g.GamersGames)
                .FirstOrDefaultAsync();

            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (game == null || user == null)
            {
                return View(nameof(MyZone));
            }

            GamerGame? gamerGame = this.context
                .GamersGames
                .Where(g => g.GameId == game.Id && g.GamerId == user)
                .FirstOrDefault();

            context.GamersGames.Remove(gamerGame);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(MyZone));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await this.context
                .Games
                .Where(g => g.Id == id)
                .Select(g => new DetailsViewModel()
                {
                    Id = g.Id,
                    Description = g.Description,
                    Genre = g.Genre.Name,
                    ImageUrl = g.ImageUrl,
                    Publisher = g.Publisher.UserName,
                    ReleasedOn = g.ReleasedOn.ToString(DateTimeFormat),
                    Title = g.Title
                })
                .FirstOrDefaultAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await this.context
                .Games
                .Where(g => g.Id == id)
                .Select(g => new AddGameInputModel()
                {
                    Description = g.Description,
                    ImageUrl = g.ImageUrl,
                    ReleasedOn = g.ReleasedOn.ToString(DateTimeFormat),
                    Title = g.Title,
                    GenreId = g.GenreId,
                })
                .FirstOrDefaultAsync();
            
            model.Genres = await GetGenres();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddGameInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            model.Genres = await GetGenres();

            DateTime correctDateTime;

            bool isDateTimeCorrect = DateTime.TryParseExact
                (model.ReleasedOn, DateTimeFormat, CultureInfo.CurrentCulture, DateTimeStyles.None, out correctDateTime);

            if (!isDateTimeCorrect)
            {
                return this.View(model);
            }

            Game? game = await this.context
                .Games
                .Where(g => g.Id == id)
                .FirstOrDefaultAsync();

            game.Title = model.Title;
            game.Description = model.Description;
            game.ImageUrl = model.ImageUrl;
            game.ReleasedOn = correctDateTime;
            game.GenreId = model.GenreId;

            await this.context.SaveChangesAsync();

            return this.RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await this.context
                .Games
                .Where(g => g.Id == id)
                .Select(g => new DeleteViewModel()
                {
                    Id = id,
                    Title = g.Title,
                    Publisher = g.Publisher.UserName,
                })
                .FirstOrDefaultAsync();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id, DeleteViewModel model)
        {
            var game = await this.context
                .Games
                .Where(g => g.Id == id)
                .FirstOrDefaultAsync();

            this.context
                .Games
                .Remove(game);
            await this.context.SaveChangesAsync();

            return this.RedirectToAction(nameof(All));
        }

        private async Task<List<Genre>> GetGenres()
        {
            var genres = new List<Genre>();
            return genres = await context.Genres
                .ToListAsync();
        }
    }
}
