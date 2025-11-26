using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers {
    public class AdminController : Controller {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService) {
            _adminService = adminService;
        }

        [HttpGet]
        public async Task<IActionResult> TopMovies(DateTime? fromDate, DateTime? toDate) {
            // Check if user is admin (UserId == "1")
            var userId = HttpContext.Session.GetString("UserId");
            if (userId != "1") {
                return RedirectToAction("Index", "Home");
            }

            var topMovies = await _adminService.GetTopMovies(fromDate, toDate);

            ViewBag.FromDate = fromDate?.ToString("yyyy-MM-dd");
            ViewBag.ToDate = toDate?.ToString("yyyy-MM-dd");

            return View(topMovies);
        }

        [HttpGet]
        public IActionResult CreateMovie() {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId != "1") {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie(CreateMovieViewModel model) {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId != "1") {
                return RedirectToAction("Index", "Home");
            }

            if (!ModelState.IsValid) {
                return View(model);
            }

            try {
                var userName = HttpContext.Session.GetString("UserName") ?? "Admin";
                var movie = await _adminService.CreateMovie(model, userName);

                TempData["SuccessMessage"] = $"Movie '{movie.Title}' created successfully!";
                return RedirectToAction("Index", "Home");
            } catch (Exception ex) {
                ModelState.AddModelError("", $"Error creating movie: {ex.Message}");
                return View(model);
            }
        }
    }
}
