using Microsoft.AspNetCore.Mvc;

namespace Waffles_Club.Areas.Admin.Controllers
{
    public class WaffleTypeController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
