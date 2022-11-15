using Movies.Api.ClientLibrary;
using Movies.Api.ClientLibrary.Models;
using Polly;
using System.Net;
using Web.Services.Abstract;

namespace Web.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly IMoviesApi _moviesApi;
        private string _token = "Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6Ijk5Mzg4NDhCMUI5Qjc3NEREMUVDMDZCNjJGNzcxRTA0IiwidHlwIjoiYXQrand0In0.eyJuYmYiOjE2NjA4Mjc5ODEsImV4cCI6MTY2MDgzMTU4MSwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NTAwNSIsImNsaWVudF9pZCI6Ik1vdmllcy5DbGllbnQiLCJqdGkiOiI5MUJGQjRCRjFDQTZCNUE1N0YyRkUxN0Y3OERFNjAwOCIsImlhdCI6MTY2MDgyNzk4MSwic2NvcGUiOlsiTW92aWUuQVBJIl19.IoL7WaHWubprOjQzRwIVeNV2zurlceNbZ_-xdKKDnVgHDZOKXcCGFKtDh5livguxr5lZkColjed1lqewVnQMpreNHoMqHQvaZgPbIN_T2FGJQ4_54sQ8rLB1S-ZgAKt_0-mAdyCSZpKr6M7Gn3L_LrbruRfw4cfgIG7Fc283MIfO1wYsL4CaSnn25g82n2c_iiP5eI747EGzG1KKOrLtijlYDc0yc7NcKYMlKT8_9ufEndzzboA1kCw8L3qerChtqOJ1jyi2h2M12D1Zl3goPV6G5CuQwmji7-BmSsenTWHpZZgP6fYQEMTwSLK7sRtZWP3TlGgUgbpXEqclZBCEnQ";
        public MoviesService(IMoviesApi moviesApi)
        {
            _moviesApi = moviesApi;
        }

        public async Task<Movie> GetMovieByIdAsync(Guid movieId)
        {
            var result = await MakeRequest(() => _moviesApi.GetMovieByIdAsync(movieId, _token));

            return result;
        }

        public async Task<List<Movie>> GetMoviesAsync()
        {
            var result = await MakeRequest(() => _moviesApi.GetMoviesAsync(_token));

            return result;
        }

        public async Task<Movie> CreateMovieAsync(Movie movie)
        {
            var result = await MakeRequest(() => _moviesApi.CreateMovieAsync(movie, _token));

            return result;
        }

        public async Task UpdateMovieAsync(Guid movieId, Movie movie)
        {
            await MakeRequest(() => _moviesApi.UpdateMovieByIdAsync(movieId, movie, _token));
        }
        
        public async Task DeleteMovieAsync(Guid movieId)
        {
            await MakeRequest(() => _moviesApi.DeleteMovieByIdAsync(movieId, _token));
        }

        private async Task<T> MakeRequest<T>(Func<Task<T>> loadingFunction)
        {
            Exception exception = null;
            var result = default(T);

            try
            {
                result = await Policy.Handle<WebException>().Or<HttpRequestException>()
                    .WaitAndRetryAsync(3, i => TimeSpan.FromMilliseconds(300), (ex, span) => exception = ex)
                    .ExecuteAsync(loadingFunction);
            }
            catch (Exception e)
            {
                // Сюда приходят ошибки вроде отсутствия интернет-соединения или неправильной работы DNS
                exception = e;
            }
            //TODO: Обработать исключения или передать их дальше            
            return result;
        }
        
        private async Task MakeRequest(Func<Task> loadingFunction)
        {
            Exception exception = null;

            try
            {
                await Policy.Handle<WebException>().Or<HttpRequestException>()
                    .WaitAndRetryAsync(3, i => TimeSpan.FromMilliseconds(300), (ex, span) => exception = ex)
                    .ExecuteAsync(loadingFunction);
            }
            catch (Exception e)
            {
                // Сюда приходят ошибки вроде отсутствия интернет-соединения или неправильной работы DNS
                exception = e;
            }
        }
    }
}
