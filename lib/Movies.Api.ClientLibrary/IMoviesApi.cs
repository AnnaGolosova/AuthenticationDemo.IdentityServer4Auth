using Movies.Api.ClientLibrary.Models;
using Refit;

namespace Movies.Api.ClientLibrary
{
    [Headers("Accept: application/json")]
    public interface IMoviesApi
    {
        [Get("/api/v1/movies")]
        Task<List<Movie>> GetMoviesAsync([Header("Authorization")] string authToken);

        [Get("/api/v1/movies/{movieId}")]
        Task<Movie> GetMovieByIdAsync(Guid movieId, [Header("Authorization")] string authToken);

        [Put("/api/v1/movies/{movieId}")]
        Task UpdateMovieByIdAsync(Guid movieId, [Body] Movie movie, [Header("Authorization")] string authToken);

        [Post("/api/v1/movies")]
        Task<Movie> CreateMovieAsync([Body] Movie movie, [Header("Authorization")] string authToken);

        [Delete("/api/v1/movies/{movieId}")]
        Task DeleteMovieByIdAsync(Guid movieId, [Header("Authorization")] string authToken);
    }
}
