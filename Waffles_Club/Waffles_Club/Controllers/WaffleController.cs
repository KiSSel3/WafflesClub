using Microsoft.AspNetCore.Mvc;

namespace Waffles_Club.Controllers
{
	public class WaffleController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
