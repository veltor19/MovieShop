using System.Diagnostics;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Models;


namespace MovieShopMVC.Controllers {
    public class HomeController : Controller {
        private readonly IMovieService _movieService;
        public HomeController(IMovieService movieService) {
            _movieService = movieService;
        }

        public async Task<IActionResult> Index(int? genreId, int page = 1, int pageSize = 12) {
            List<MovieCardModel> movies = new List<MovieCardModel>();

            movies = await _movieService.GetTop20GrossingMovies();
  
            return View(movies);
        }

        [HttpGet]
        public IActionResult TestException() {
            throw new InvalidOperationException("Testing Serilog exception logging!");
        }

        public async Task<IActionResult> MoviesByGenre(int genreId, int pageSize = 30, int pageNumber = 1) {
            var paginatedMovies = await _movieService.GetMoviesByGenrePaginated(genreId, pageSize, pageNumber);
            return View(paginatedMovies);
        }

        public IActionResult Privacy() {
            //ViewData["Key"] = Vaue
            //IDictionary<string, object>
            ViewData["Message"] = "Hello from ViewData";
            ViewData["Age"] = 30;
            ViewBag.Message1 = "Hello from ViewBag";
            ViewBag.Age1 = 35;
            return View();
        }

        public IActionResult TopMovies() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
