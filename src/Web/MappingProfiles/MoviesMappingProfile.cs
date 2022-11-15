using AutoMapper;
using Movies.Api.ClientLibrary.Models;
using Web.Models;

namespace Web.MappingProfiles
{
    public class MoviesMappingProfile : Profile
    {
        public MoviesMappingProfile()
        {
            CreateMap<Movie, MovieViewModel>().ReverseMap();
        }
    }
}
