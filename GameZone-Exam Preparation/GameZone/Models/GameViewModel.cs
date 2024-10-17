using Microsoft.AspNetCore.Identity;

namespace GameZone.Models
{
    public class GameViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string ReleasedOn { get; set; }
        public string ImageUrl { get; set; }
        public string Publisher { get; set; }
    }
}
