using DeskMarket.Data;
using DeskMarket.Data.Models;
using DeskMarket.Models.Input;
using DeskMarket.Models.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;

using static DeskMarket.Common.ApplicationConstants;

namespace DeskMarket.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext context;
        public ProductController(ApplicationDbContext _dbContext)
        {
            context = _dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = GetUser();

            var model = await this.context
                .Products
                .Where(p => p.IsDeleted == false)
                .Select(p => new ProductViewModel()
                {
                    Id = p.Id,
                    HasBought = false,
                    ImageUrl = p.ImageUrl,
                    IsSeller = p.SellerId == GetUser(),
                    Price = p.Price,
                    ProductName = p.ProductName,
                })
                .ToListAsync();

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new ProductInputModel();
            model.Categories = await this.context.Categories.ToListAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductInputModel model)
        {
            model.Categories = await GetCategories();

            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            DateOnly addedOnDate;

            bool isDateTimeValid = DateOnly.TryParseExact(model.AddedOn, DateTimeGeneralFormat, CultureInfo.CurrentCulture, DateTimeStyles
                .None, out addedOnDate);

            var user = GetUser();

            if (!isDateTimeValid) 
            {
                return this.View(model);
            }

            Product product = new Product()
            {
                ProductName = model.ProductName,
                AddedOn = addedOnDate,
                CategoryId = model.CategoryId,
                Description = model.Description,
                Price = model.Price,
                ImageUrl = model.ImageUrl,
                SellerId = user,
                IsDeleted = false,
            };

            await this.context
                .Products
                .AddAsync(product);

            await this.context
                .SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var product = this.context
                .Products
                .Include(p => p.Category)
                .Include(p => p.Seller)
                .Include(p => p.ProductsClients)
                .Where(p => p.Id == id)
                .FirstOrDefault();

            var categories = GetCategories();
            var user = GetUser();

            ProductDetailsViewModel model = new ProductDetailsViewModel();

            model = new ProductDetailsViewModel()
            {
                Id = product.Id,
                AddedOn = product.AddedOn.ToString(),
                CategoryName = product.Category.Name,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Seller = product.Seller.UserName,
                HasBought = model.Seller == GetUser()
            };

            return this.View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await this.context
                .Products
                .Include(p => p.Category)
                .Include(p => p.Seller)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();

            var categories = await GetCategories();

            ProductEditViewModel model = new ProductEditViewModel()
            {
                ProductName = product.ProductName,
                Price = product.Price,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                AddedOn = product.AddedOn,
                CategoryId = product.Category.Id,
                Categories = categories,
                SellerId = product.SellerId,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Product? product = await this.context
                .Products
                .Include(p => p.Category)
                .Include(p => p.Seller)
                .Where(product => product.Id == id)
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return BadRequest();
            }

            product.ProductName = model.ProductName;
            product.Price = model.Price;
            product.Description = model.Description;
            product.ImageUrl = model.ImageUrl;
            product.AddedOn = model.AddedOn;
            product.CategoryId = model.CategoryId;
            product.SellerId = model.SellerId;

            await this.context.SaveChangesAsync();

            return this.RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToCart(int id)
        {
            var product = await this.context
                .Products
                .FindAsync(id);

            var user = GetUser();

            if (context.ProductsClients.Any(pc => pc.ProductId == product.Id) &&
                context.ProductsClients.Any(pc => pc.ClientId == user))
            {
                return this.RedirectToAction(nameof(Cart));
            }

            if (product == null)
            {
                return BadRequest();
            }
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(Index));
            }

            ProductClients productClient = new ProductClients()
            {
                ProductId = product.Id,
                ClientId = GetUser(),
            };

            await this.context
                .ProductsClients
                .AddAsync(productClient);

            await this.context.SaveChangesAsync();

            return this.RedirectToAction(nameof(Cart));
        }

        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            var user = GetUser();

            var model = await this.context
                .Products
                .Where(pc => pc.ProductsClients.Any(p => p.ClientId == user) &&
                    pc.IsDeleted == false)
                .Select(x => new ProductCartViewModel()
                {
                    Id = x.Id,
                    ProductName = x.ProductName,
                    Price = x.Price,
                    ImageUrl = x.ImageUrl,
                })
                .ToListAsync();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var productClient = await this.context
                .ProductsClients
                .Where(pc => pc.ProductId == id)
                .FirstOrDefaultAsync();

            this.context
                .ProductsClients
                .Remove(productClient);
            await this.context.SaveChangesAsync();

            return this.RedirectToAction(nameof(Cart));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await this.context
                .Products
                .Include(p => p.Seller)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();

            ProductDeleteViewModel model = new ProductDeleteViewModel()
            {
                Id = product.Id,
                ProductName = product.ProductName,
                SellerId = product.SellerId,
                Seller = product.Seller.UserName,
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Product product)
        {
            var currentProduct = await this.context
                .Products
                .Where(p => p.Id == product.Id)
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return this.RedirectToAction(nameof(Index));
            }

            currentProduct.IsDeleted = true;
            await this.context.SaveChangesAsync();

            return this.RedirectToAction(nameof(Index));
        }

        private string GetUser()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        }

        private async Task<List<Category>> GetCategories()
        {
            var categories = await this.context
                .Categories
                .ToListAsync();

            return categories;
        }
    }
}
