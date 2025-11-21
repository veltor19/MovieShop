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

        public IActionResult Index() {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString)) {
                return RedirectToAction("Login", "User");
            }
            var userId = int.Parse(userIdString);

            var model = _accountService.GetUserAccountDetails(userId);
            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateProfile(UserAccountModel model) {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString)) {
                return RedirectToAction("Login", "User");
            }
            var userId = int.Parse(userIdString);

            var success = _accountService.UpdateProfile(userId, model);

            if (success) {
                TempData["SuccessMessage"] = "Profile updated successfully!";
            } else {
                TempData["ErrorMessage"] = "Failed to update profile.";
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult PurchaseMovie(int id) {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString)) {
                return RedirectToAction("Login", "User");
            }

            var userId = int.Parse(userIdString);
            var result = _purchaseService.PurchaseMovie(userId, id);

            if (result) {
                TempData["SuccessMessage"] = "Movie purchased successfully!";
            } else {
                TempData["ErrorMessage"] = "You already own this movie or purchase failed.";
            }

            return RedirectToAction("MovieDetails", "Movies", new { id });
        }

        [HttpPost]
        public IActionResult ToggleFavorite(int id) {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString)) {
                return RedirectToAction("Login", "User");
            }

            var userId = int.Parse(userIdString);
            _favoriteService.ToggleFavorite(userId, id);

            return RedirectToAction("MovieDetails", "Movies", new { id });
        }

        public IActionResult Movies() {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString)) {
                return RedirectToAction("Login", "User");
            }

            var userId = int.Parse(userIdString);
            var purchases = _purchaseService.GetUserPurchases(userId);
            var movieCards = purchases.Select(p => new MovieCardModel {
                Id = p.Movie.Id,
                Title = p.Movie.Title,
                PosterURL = p.Movie.PosterUrl
                // Map other properties as needed
            }).ToList();

            return View(movieCards);
        }

        public IActionResult Favorites() {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString)) {
                return RedirectToAction("Login", "User");
            }

            var userId = int.Parse(userIdString);
            var favorites = _favoriteService.GetUserFavorites(userId);
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
