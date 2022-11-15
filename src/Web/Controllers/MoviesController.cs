using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.Api.ClientLibrary.Models;
using Web.Models;
using Web.Services.Abstract;

namespace Web.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _moviesService;
        private readonly IMapper _mapper;

        public MoviesController(IMoviesService moviesService, IMapper mapper)
        {
            _moviesService = moviesService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var moviesList = await _moviesService.GetMoviesAsync();

            return moviesList != null ? 
                          View(_mapper.Map<List<MovieViewModel>>(moviesList)) :
                          Problem("Movies cannot be found!");
        }

        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound("Provided id cannot be empty!");
            }

            var movieModel = await _moviesService.GetMovieByIdAsync(id);
            
            if (movieModel == null)
            {
                return NotFound("Movie with provided id cannot be found!");
            }

            return View(movieModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Genre,ReleaseDate,Director,Writer,ImageUrl")] MovieViewModel movieModel)
        {
            if (ModelState.IsValid)
            {
                movieModel.Id = Guid.NewGuid();
                
                await _moviesService.CreateMovieAsync(_mapper.Map<Movie>(movieModel));
                
                return RedirectToAction(nameof(Details), movieModel.Id);
            }
            return View(movieModel);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound("Provided id cannot be empty!");
            }

            var movie = await _moviesService.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound("Movie with provided id cannot be found!");
            }
            return View(_mapper.Map<MovieViewModel>(movie));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Genre,ReleaseDate,Director,Writer,ImageUrl")] MovieViewModel movieModel)
        {
            if (id != movieModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _moviesService.UpdateMovieAsync(id, _mapper.Map<Movie>(movieModel));
                
                return RedirectToAction(nameof(Index));
            }
            return View(movieModel);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound("Provided id cannot be empty!");
            }

            var movie = await _moviesService.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound("Movie with provided id cannot be found!");
            }
            return View(_mapper.Map<MovieViewModel>(movie));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _moviesService.DeleteMovieAsync(id);
        
            return RedirectToAction(nameof(Index));
        }
    }
}
