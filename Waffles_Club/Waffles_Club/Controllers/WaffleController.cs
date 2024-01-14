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
		public async Task<IActionResult> Index(string? waffleName = null, Guid? waffleTypeId = null, Guid? fillingTypeId = null,
                                               decimal minPrice = 0, decimal maxPrice = 100, int pageNow = 1)
		{
			try
			{
				var viewModel = new HomePageViewModel();

				var waffleList = await _waffleService.GetWaffleListAsync(waffleName, waffleTypeId, fillingTypeId, minPrice, maxPrice, pageNow);
				viewModel.Waffles = waffleList;

				var waffleTypeList = await _waffleTypeService.GetAllAsync();
				viewModel.WaffleTypes = waffleTypeList;

                var fillingTypeList = await _fillingTypeService.GetAllAsync();
				viewModel.FillingTypes = fillingTypeList;

				viewModel.CurrentWaffleName = waffleName;

				viewModel.CurrentWaffleTypeId = waffleTypeId;
				if(waffleTypeId != null)
				{
					var currentWaffleType = await _waffleTypeService.GetByIdAsync(waffleTypeId.Value);
                    viewModel.CurrentWaffleTypeName = currentWaffleType.Name;
                }
				else
				{
                    viewModel.CurrentWaffleTypeName = "Все";
                }

				viewModel.CurrentFillingTypeId = fillingTypeId;
                if (fillingTypeId != null)
                {
                    var currentFillingType = await _fillingTypeService.GetByIdAsync(fillingTypeId.Value);
                    viewModel.CurrentFillingTypeName = currentFillingType.Name;
                }
                else
                {
                    viewModel.CurrentFillingTypeName = "Все";
                }

                viewModel.CurrentMinPrice = minPrice;
				viewModel.CurrentMaxPrice = maxPrice;

                return View(viewModel);
			}
			catch(Exception ex)
			{
                return View("Error", new ErrorViewModel() { RequestId = ex.Message });
            }
		}
    }
}
