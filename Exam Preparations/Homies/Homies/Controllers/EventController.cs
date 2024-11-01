using Homies.Data;
using Homies.Data.Models;
using Homies.Models;
using Homies.Models.Input;
using Homies.Models.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;
using static Homies.Common.ApplicationConstants;

namespace Homies.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly HomiesDbContext context;

        public EventController(HomiesDbContext _dbContext)
        {
            this.context = _dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await this.context
                .Events
                .Select(e => new EventViewModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Organiser = e.Organiser.UserName,
                    Start = e.Start.ToString(DateTimeGeneralFormat),
                    Type = e.Type.Name,
                })
                .ToListAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new EventInputModel();
            model.Types = await GetTypes();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(EventInputModel model)
        {
            model.Types = await GetTypes();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            DateTime startDate;
            DateTime endDate;

            bool isStartDateValid = DateTime.TryParseExact(model.Start, DateTimeGeneralFormat, CultureInfo.CurrentCulture, DateTimeStyles.None, out startDate);
            bool isEndDateValid = DateTime.TryParseExact(model.End, DateTimeGeneralFormat, CultureInfo.CurrentCulture, DateTimeStyles.None, out endDate);


            if (!isStartDateValid && !isEndDateValid)
            {
                return View(model);
            }

            Event newEvent = new Event()
            {
                Name = model.Name,
                Description = model.Description,
                OrganiserId = GetUser(),
                CreatedOn = DateTime.Now,
                Start = startDate,
                End = endDate,
                TypeId = model.TypeId,
            };

            await this.context.AddAsync(newEvent);
            await this.context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await this.context
                .Events
                .FindAsync(id);

            string currentUser = GetUser();

            if (currentUser != model.OrganiserId)
            {
                return this.RedirectToAction(nameof(All));
            }

            EventInputModel eventInputModel = new EventInputModel()
            {
                Name = model.Name,
                Description = model.Description,
                Start = model.Start.ToString(),
                End = model.End.ToString(),
                TypeId = model.TypeId,
            };
            eventInputModel.Types = await GetTypes();

            return View(eventInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EventInputModel model)
        {
            model.Types = await GetTypes();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            DateTime startDate;
            DateTime endDate;

            bool isStartDateValid = DateTime.TryParseExact(model.Start, DateTimeGeneralFormat, CultureInfo.CurrentCulture, DateTimeStyles.None, out startDate);
            bool isEndDateValid = DateTime.TryParseExact(model.End, DateTimeGeneralFormat, CultureInfo.CurrentCulture, DateTimeStyles.None, out endDate);


            if (!isStartDateValid && !isEndDateValid)
            {
                return View(model);
            }

            Event? eventToEditing = await this.context
                .Events
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();

            if (eventToEditing == null)
            {
                return BadRequest();
            }

            eventToEditing.Name = model.Name;
            eventToEditing.Description = model.Description;
            eventToEditing.Start = startDate;
            eventToEditing.End = endDate;
            eventToEditing.TypeId = model.TypeId;
                
            await this.context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            var model = await this.context
                .Events
                .Where(e => e.EventsParticipants.Any(ep => ep.HelperId == GetUser()))
                .Select(e => new EventViewModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Organiser = e.Organiser.UserName,
                    Start = e.Start.ToString(JoinedEventsDateFormat),
                    Type = e.Type.Name,
                })
                .ToListAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            Event? eventToAdd = await this.context
                .Events
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();

            var user = GetUser();

            EventParticipant eventParticipant = new EventParticipant()
            {
                EventId = eventToAdd.Id,
                HelperId = user,
            };

            if (context.EventsParticipants.Any(ep => ep.EventId == id) 
                && context.EventsParticipants.Any(ep => ep.HelperId == user))
            {
                return RedirectToAction(nameof(All));
            }

            await this.context
                .EventsParticipants
                .AddAsync(eventParticipant);

            await this.context.SaveChangesAsync();

            return RedirectToAction(nameof(Joined));
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            EventParticipant? modelToRemove = await this.context
                .EventsParticipants
                .Where(ep => ep.EventId == id)
                .FirstOrDefaultAsync();

            if (modelToRemove == null)
            {
                return this.RedirectToAction(nameof(Joined));
            }

            this.context
                .EventsParticipants
                .Remove(modelToRemove);

            await this.context.SaveChangesAsync();

            return this.RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Event? currentEvent = await this.context
                .Events
                .Include(e => e.Organiser)
                .Include(e => e.Type)
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();

            if (currentEvent == null)
            {
                return BadRequest();
            }

            DetailsViewModel model = new DetailsViewModel()
            {
                Id = currentEvent.Id,
                Name = currentEvent.Name,
                Description = currentEvent.Description,
                Organiser = currentEvent.Organiser.Email,
                CreatedOn = currentEvent.CreatedOn.ToString(DetailsDateFormat),
                Start = currentEvent.Start.ToString(DetailsDateFormat),
                End = currentEvent.End.ToString(DetailsDateFormat),
                Type = currentEvent.Type.Name,
            };

            return this.View(model);
        }

        private async Task<List<Data.Models.Type>> GetTypes()
        {
            return await this.context
                .Types
                .ToListAsync();
        }

        private string GetUser()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}