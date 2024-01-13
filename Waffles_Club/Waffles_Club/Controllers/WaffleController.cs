using Microsoft.AspNetCore.Mvc;
using Waffles_Club.Service.Services.Interfaces;
using Waffles_Club.Shared.ViewModels;

namespace Waffles_Club.Controllers
{
	public class WaffleController : Controller
	{
		private readonly IWaffleService _waffleService;

		private readonly IConfiguration _configuration;

		public WaffleController(IWaffleService waffleService, IConfiguration configuration)
		{
			_waffleService = waffleService;
			_configuration = configuration;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}
	}
}
