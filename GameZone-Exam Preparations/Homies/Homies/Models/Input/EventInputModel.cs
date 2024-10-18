using System.ComponentModel.DataAnnotations;
using static Homies.Common.ApplicationConstants;

namespace Homies.Models.Input
{
    public class EventInputModel
    {
        [Required]
        [MinLength(EventNameMinLength, ErrorMessage = "Minimum length of name is 5")]
        [MaxLength(EventNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(EventDescriptionMinLength, ErrorMessage = "Minimum length of description is 15")]
        [MaxLength(EventDescriptionMaxLength)]
        public string Description { get; set; }

        public string Start { get; set; }
        public string End { get; set; }
        public int TypeId { get; set; }
        public List<Data.Models.Type>? Types { get; set; }
    }
}
