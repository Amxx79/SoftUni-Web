using DeskMarket.Data.Models;
using System.ComponentModel.DataAnnotations;
using static DeskMarket.Common.ApplicationConstants;

namespace DeskMarket.Models.Output
{
    public class ProductEditViewModel
    {
        public string ProductName { get; set; }
        [Range(ProductPriceMinValue, ProductPriceMaxValue)]
        public decimal Price { get; set; }

        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateOnly AddedOn { get; set; }
        public int CategoryId { get; set; }
        public List<Category>? Categories { get; set; }
        public string SellerId { get; set; }
    }
}
