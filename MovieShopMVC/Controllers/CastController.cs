using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers {
    public class CastController : Controller {

        private readonly ICastService _castService;

        public CastController(ICastService castService) {
            _castService = castService;
        }
        public IActionResult Index() {
            return View();
        }

        public async Task<IActionResult> Details(int id) {
            var cast = await _castService.GetCastDetails(id);

            if (cast == null) {
                return NotFound();   // or return View("NotFound");
            }

            return View(cast);   // returns Details.cshtml with cast data
        }
    }
}
