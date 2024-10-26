using System.ComponentModel.DataAnnotations;
using static DeskMarket.Common.ApplicationConstants;

namespace DeskMarket.Data.Models
{
    public class Category
    {
        [Key]
        public int Id{ get; set; }
        public string Name { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}