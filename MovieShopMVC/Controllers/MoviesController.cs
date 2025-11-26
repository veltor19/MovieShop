using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers {
    public class MoviesController : Controller {
        private readonly IMovieService _movieService;
        private readonly IReviewService _reviewService;
        public MoviesController(IMovieService movieService, IReviewService reviewService) {
            _movieService = movieService;
            _reviewService = reviewService;
        }
        public IActionResult Index() {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> MovieDetails(int id) {
            var userIdString = HttpContext.Session.GetString("UserId");
            int? userId = null;
            if (!string.IsNullOrEmpty(userIdString)) {
                userId = int.Parse(userIdString);
            }
            var movie = await _movieService.GetMovieDetails(id, userId);
            return View(movie);
        }


        [HttpPost]
        public async Task<IActionResult> SubmitReview(int id, decimal rating, string reviewText) {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString)) {
                return RedirectToAction("Login", "User");
            }

            var userId = int.Parse(userIdString);
            var result = await _reviewService.CreateReview(userId, id, rating, reviewText);

            if (result) {
                TempData["SuccessMessage"] = "Review submitted successfully!";
            } else {
                TempData["ErrorMessage"] = "You have already reviewed this movie.";
            }

            return RedirectToAction("MovieDetails", new { id });
        }
    }
}
