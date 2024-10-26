using DeskMarket.Data.Models;
using System.ComponentModel.DataAnnotations;
using static DeskMarket.Common.ApplicationConstants;

namespace DeskMarket.Models.Input
{
    public class ProductInputModel
    {
        public string ProductName { get; set; }
        [Range(ProductPriceMinValue, ProductPriceMaxValue)]
        public decimal Price { get; set; }

        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string AddedOn { get; set; }
        public int CategoryId { get; set; }
        public List<Category>? Categories { get; set; }
    }
}
