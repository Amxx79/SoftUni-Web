using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeskMarket.Data.Models
{
    public class ProductClients
    {
        [Required]
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        [Required]
        public string ClientId { get; set; }
        [ForeignKey(nameof(ClientId))]
        public IdentityUser Client { get; set; }

    }
}
