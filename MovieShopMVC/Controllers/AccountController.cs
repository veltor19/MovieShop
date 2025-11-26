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


        [HttpPost]
        public async Task<IActionResult> PurchaseMovie(int id) {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString)) {
                return RedirectToAction("Login", "User");
            }

            var userId = int.Parse(userIdString);
            var result = await _purchaseService.PurchaseMovie(userId, id);

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
