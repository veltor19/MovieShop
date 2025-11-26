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
        public async Task<IActionResult> Login(LoginRequestModel model) {

            var user = await _userService.ValidateUser(model.Email, model.Password);
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
        public async Task<IActionResult> Register(RegisterRequestModel model) {
            var existingUser = await _userService.GetUserByEmail(model.Email);
            if (existingUser != null) {
                ModelState.AddModelError("Email", "Email already exists");
                return View(model);
            }

            var result = await _userService.RegisterUser(model);
            if (result) {
                var user = await _userService.GetUserByEmail(model.Email);
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
