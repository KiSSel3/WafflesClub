using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Waffles_Club.Service.Services.Interfaces;
using Waffles_Club.Shared.ViewModels;

namespace Waffles_Club.Controllers
{
	public class WaffleController : Controller
    {
		private readonly IWaffleService _waffleService;
		private readonly IWaffleTypeService _waffleTypeService;
		private readonly IFillingTypeService _fillingTypeService;

		private readonly IConfiguration _configuration;

		public WaffleController(IWaffleService waffleService, IWaffleTypeService waffleTypeService, IFillingTypeService fillingTypeService, IConfiguration configuration)
		{
			_waffleService = waffleService;
			_waffleTypeService = waffleTypeService;
			_fillingTypeService = fillingTypeService;

			_configuration = configuration;
		}

		[HttpGet]
		public async Task<IActionResult> Index(int pageNow = 1)
		{
			try
			{
				var viewModel = new HomeIndexViewModel();

				var waffleList = await _waffleService.GetWaffleListAsync(pageNow: pageNow);
				viewModel.Waffles = waffleList;

				var waffleTypeList = await _waffleTypeService.GetAllAsync();
				viewModel.SelectListWaffleType = new SelectList(waffleTypeList, "Id", "Name");

                var fillingTypeList = await _fillingTypeService.GetAllAsync();
                viewModel.SelectListFillingType = new SelectList(fillingTypeList, "Id", "Name");

                return View(viewModel);
			}
			catch(Exception ex)
			{
                return View("Error", new ErrorViewModel() { RequestId = ex.Message });
            }
		}
    }
}
