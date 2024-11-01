using System.ComponentModel.DataAnnotations;
using static Homies.Common.ApplicationConstants;

namespace Homies.Data.Models
{
    public class Type
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(TypeNameMaxLength)]
        public string Name { get; set; }
        public IList<Event> Events { get; set; } = new List<Event>();
    }
}
