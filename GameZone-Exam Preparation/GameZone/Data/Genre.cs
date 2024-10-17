using System.ComponentModel.DataAnnotations;
using static GameZone.Common.ApplicationConstants;

namespace GameZone.Data
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(GenreTitleMaxLength)]
        public string Name { get; set; }

        public ICollection<Game> Games { get; set; } = new List<Game>();
    }
}