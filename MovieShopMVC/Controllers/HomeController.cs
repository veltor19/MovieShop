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

        public IActionResult Index(int? genreId) {
            //var movieService = new MovieService();
            List<MovieCardModel> movies = new List<MovieCardModel>();
            if (genreId.HasValue) {
                movies = _movieService.GetMovieByGenre(genreId);
            } else {
                movies = _movieService.GetTop20GrossingMovies();
            }
            return View(movies);
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
