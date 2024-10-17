using GameZone.Data;

namespace GameZone.Models
{
    public class AddGameInputModel
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }

        //CHECK IT
        public string ReleasedOn { get; set; }
        public int GenreId { get; set; }
        public List<Genre>? Genres { get; set; }
    }
}
