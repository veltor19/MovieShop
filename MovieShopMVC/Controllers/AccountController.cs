using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers {
    public class AccountController : Controller {
        private readonly IPurchaseService _purchaseService;
        private readonly IFavoriteService _favoriteService;
        private readonly IAccountService _accountService;

        public AccountController(IPurchaseService purchaseService, IFavoriteService favoriteService, IAccountService accountService) {
            _purchaseService = purchaseService;
            _favoriteService = favoriteService;
            _accountService = accountService;
        }

        public async Task<IActionResult> Index() {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString)) {
                return RedirectToAction("Login", "User");
            }
            var userId = int.Parse(userIdString);

            var model = await _accountService.GetUserAccountDetails(userId);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UserAccountModel model) {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString)) {
                return RedirectToAction("Login", "User");
            }
            var userId = int.Parse(userIdString);

            var success = await _accountService.UpdateProfile(userId, model);

            if (success) {
                TempData["SuccessMessage"] = "Profile updated successfully!";
            } else {
                TempData["ErrorMessage"] = "Failed to update profile.";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> TestPurchase(int movieId, string date) {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString)) {
                return Json(new { error = "Not logged in" });
            }

            DateTime testDate;
            if (!DateTime.TryParse(date, out testDate)) {
                return Json(new { error = "Invalid date format" });
            }

            var userId = int.Parse(userIdString);
            var result = await _purchaseService.PurchaseMovie(userId, movieId, testDate);

            return Json(new {
                success = result,
                date = testDate.ToString("yyyy-MM-dd"),
                message = result ? "Purchase succeeded" : "Purchase failed (past date or already owned)"
            });
        }


        [HttpPost]
        public async Task<IActionResult> PurchaseMovie(int id, DateTime? purchaseDate = null) {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString)) {
                return RedirectToAction("Login", "User");
            }

            // Use provided date or default to today
            var actualPurchaseDate = purchaseDate ?? DateTime.Now;

            // Manual validation using the same logic as the attribute
            if (actualPurchaseDate.Date < DateTime.Today) {
                TempData["ErrorMessage"] = "Purchase date cannot be earlier than today's date.";
                return RedirectToAction("MovieDetails", "Movies", new { id });
            }

            var userId = int.Parse(userIdString);
            var result = await _purchaseService.PurchaseMovie(userId, id, actualPurchaseDate);

            if (result) {
                TempData["SuccessMessage"] = "Movie purchased successfully!";
            } else {
                TempData["ErrorMessage"] = "You already own this movie or purchase failed.";
            }

            return RedirectToAction("MovieDetails", "Movies", new { id });
        }

        [HttpPost]
        public async Task<IActionResult> ToggleFavorite(int id) {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString)) {
                return RedirectToAction("Login", "User");
            }

            var userId = int.Parse(userIdString);
            await _favoriteService.ToggleFavorite(userId, id);

            return RedirectToAction("MovieDetails", "Movies", new { id });
        }

        public async Task<IActionResult> Movies() {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString)) {
                return RedirectToAction("Login", "User");
            }

            var userId = int.Parse(userIdString);
            var purchases = await _purchaseService.GetUserPurchases(userId);
            var movieCards = purchases.Select(p => new MovieCardModel {
                Id = p.Movie.Id,
                Title = p.Movie.Title,
                PosterURL = p.Movie.PosterUrl
                // Map other properties as needed
            }).ToList();

            return View(movieCards);
        }

        public async Task<IActionResult> Favorites() {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString)) {
                return RedirectToAction("Login", "User");
            }

            var userId = int.Parse(userIdString);
            var favorites = await _favoriteService.GetUserFavorites(userId);
            var movieCards = favorites.Select(p => new MovieCardModel {
                Id = p.Movie.Id,
                Title = p.Movie.Title,
                PosterURL = p.Movie.PosterUrl
                // Map other properties as needed
            }).ToList();

            return View(movieCards);
        }
    }
}
