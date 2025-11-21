using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Models;

namespace MovieShopMVC.Controllers {
    public class UserController : Controller {
        private readonly IUserService _userService;

        public UserController(IUserService userService) {
            _userService = userService;
        }
        public IActionResult Index() {
            return View();
        }

        [HttpGet]
        public IActionResult Login() {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginRequestModel model) {

            var user = _userService.ValidateUser(model.Email, model.Password);
            if (user == null) {
                ModelState.AddModelError("", "Invalid email or password");
                return View(model);
            }
            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("UserName", user.FirstName + " " + user.LastName);
            HttpContext.Session.SetString("UserEmail", user.Email);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register() {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterRequestModel model) {
            var existingUser = _userService.GetUserByEmail(model.Email);
            if (existingUser != null) {
                ModelState.AddModelError("Email", "Email already exists");
                return View(model);
            }

            var result = _userService.RegisterUser(model);
            if (result) {
                var user = _userService.GetUserByEmail(model.Email);
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                HttpContext.Session.SetString("UserName", user.FirstName + " " + user.LastName);
                HttpContext.Session.SetString("UserEmail", user.Email);
                return RedirectToAction("Login");
            }

            ModelState.AddModelError("", "Registration failed");
            return View(model);
        }

        [HttpGet]
        public IActionResult Test() {
            return Content("User controller works!");
        }

     

        [HttpGet]
        public IActionResult Logout() {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
