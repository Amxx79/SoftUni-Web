using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static DeskMarket.Common.ApplicationConstants;

namespace DeskMarket.Data.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(ProductNameMaxLength)]
        public string ProductName { get; set; }

        [Required]
        [MaxLength(ProductDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        //CHECK THE VALUES
        [Range(ProductPriceMinValue, ProductPriceMaxValue)]
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }

        [Required]
        public string SellerId { get; set; }
        [ForeignKey(nameof(SellerId))]      
        public IdentityUser Seller { get; set; }

        [Required]
        public DateOnly AddedOn { get; set; }

        [Required]
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

        public bool IsDeleted { get; set; }
        public ICollection<ProductClients> ProductsClients { get; set; } = new List<ProductClients>();
    }
}
