using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Homies.Common.ApplicationConstants;

namespace Homies.Data.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(EventNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(EventDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public string OrganiserId { get; set; }
        [ForeignKey(nameof(OrganiserId))]
        public IdentityUser Organiser { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public int TypeId { get; set; }
        [ForeignKey(nameof(TypeId))]
        public Type Type { get; set; }
        public IList<EventParticipant> EventsParticipants { get; set; } = new List<EventParticipant>();
    }
}
