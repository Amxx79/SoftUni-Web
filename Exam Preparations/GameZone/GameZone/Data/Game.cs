using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static GameZone.Common.ApplicationConstants;

namespace GameZone.Data
{
    public class Game
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(GameTitleMaxLength)]
        public string Title { get; set; }

        [MaxLength(GameDescriptionMaxLength)]
        public string Description { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        public string PublisherId { get; set; }
        [ForeignKey(nameof(PublisherId))]
        public IdentityUser Publisher { get; set; }

        [Required]
        public DateTime ReleasedOn { get; set; }

        [Required]
        public int GenreId { get; set; }
        [ForeignKey(nameof(GenreId))]
        [Required]
        public Genre Genre { get; set; }

        public ICollection<GamerGame> GamersGames { get; set; } = new List<GamerGame>();
    }
}
