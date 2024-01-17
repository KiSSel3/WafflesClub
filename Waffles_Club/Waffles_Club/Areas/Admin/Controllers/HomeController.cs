using Microsoft.AspNetCore.Mvc;

namespace Waffles_Club.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
