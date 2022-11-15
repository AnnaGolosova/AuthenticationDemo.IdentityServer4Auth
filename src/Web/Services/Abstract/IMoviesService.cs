using Movies.Api.ClientLibrary.Models;

namespace Web.Services.Abstract
{
    public interface IMoviesService
    {
        Task<Movie> GetMovieByIdAsync(Guid movieId);

        Task<List<Movie>> GetMoviesAsync();

        Task<Movie> CreateMovieAsync(Movie movie);

        Task UpdateMovieAsync(Guid movieId, Movie movie);

        Task DeleteMovieAsync(Guid movieId);
    }
}
