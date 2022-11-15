namespace Movies.Api.ClientLibrary.Models
{
    public class Movie
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string ImageUrl { get; set; }
    }
}
