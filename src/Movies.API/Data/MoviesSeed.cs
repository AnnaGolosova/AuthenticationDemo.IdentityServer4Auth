using LoremIpsumGenerator.Libs;
using Movies.Api.ClientLibrary.Models;

namespace Movies.API.Data
{
    public static class MoviesSeed
    {
        private static readonly List<string> _movieImageUrls = new List<string>()
        {
            "https://b1.filmpro.ru/c/827457.224x316.jpg",
            "https://b1.filmpro.ru/c/827525.224x316.jpg",
            "https://www.film.ru/sites/default/files/styles/thumb_260x400/public/movies/posters/49688650-2200719.jpeg",
            "https://b1.filmpro.ru/c/827615.224x316.jpg",
            "https://www.film.ru/sites/default/files/styles/thumb_g_674x450/public/trailers_frame/krasotka.jpg",
            "https://upload.wikimedia.org/wikipedia/ru/7/7e/Oxygen_%282021_film%29.jpg",
            "https://avatars.mds.yandex.net/get-kinopoisk-image/1704946/acd4eb55-c3c0-4dac-9f8d-ee733e4893cc/600x900",
            "https://b1.filmpro.ru/c/827683.224x316.jpg"
        };
        
        private static async Task SeedAsync(MoviesContext moviesContext)
        {
            if (!moviesContext.Movie.Any())
            {
                var random = new Random();
                IGenerator generator = new Generator(wordsPerBlockLower: 2, wordsPerBlockHigher: 3, totalBlocks: 100);

                while (generator.CanRead)
                {
                    var newList = generator.Generate<Movie>();

                    newList.ForEach(m => m.ImageUrl = _movieImageUrls[random.Next(_movieImageUrls.Count)]);
                    
                    moviesContext.Movie.AddRange(newList);
                }
                await moviesContext.SaveChangesAsync();
            }
        }

        public static async Task SeedDatabase(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<MoviesContext>();
                SeedAsync(context).Wait();
            }
        }
    }
}
