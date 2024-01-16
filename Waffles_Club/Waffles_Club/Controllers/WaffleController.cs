using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Waffles_Club.Data.Entity;
using Waffles_Club.Data.Enum;
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
        public async Task<IActionResult> Index()
		{
            try
            {
                var viewModel = new WafflePageViewModel();
                var waffleList = await _waffleService.GetWaffleListAsync();
                viewModel.Waffles = waffleList;

                var waffleTypeList = await _waffleTypeService.GetAllAsync();
                viewModel.WaffleTypes = waffleTypeList;

                var fillingTypeList = await _fillingTypeService.GetAllAsync();
                viewModel.FillingTypes = fillingTypeList;

                viewModel.CurrentWaffleTypeName = "Все";

                viewModel.CurrentFillingTypeName = "Все";

                viewModel.CurrentMinPrice = 0;
                viewModel.CurrentMaxPrice = 100;

                return View(viewModel);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { RequestId = ex.Message });
            }
        }

       [HttpPost]
		public async Task<IActionResult> Index(string? waffleName = null, Guid? waffleTypeId = null, Guid? fillingTypeId = null,
                                               decimal minPrice = 0, decimal maxPrice = 100, int pageNow = 1, SortingParameters sortingParameters = SortingParameters.None)
		{
			try
			{
				var viewModel = new WafflePageViewModel();

				var waffleList = await _waffleService.GetWaffleListAsync(waffleName, waffleTypeId, fillingTypeId, minPrice, maxPrice, pageNow, sortingParameters: sortingParameters);
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

                viewModel.CurrentSortingParameters = sortingParameters;

                return View(viewModel);
			}
			catch(Exception ex)
			{
                return View("Error", new ErrorViewModel() { RequestId = ex.Message });
            }
		}

        public async Task<IActionResult> Details(Guid waffleId)
        {
            try
            {
                var viewModel = new WaffleDetailsViewModel();

                var waffle = await _waffleService.GetWaffleByIdAsync(waffleId);
                viewModel.Waffle = waffle;

                var waffleType = await _waffleTypeService.GetByIdAsync(waffle.TypeId);
                viewModel.WaffleType = waffleType;

                var fillingType = await _fillingTypeService.GetByIdAsync(waffle.FillingTypeId);
                viewModel.FillingType = fillingType;

                return View(viewModel);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { RequestId = ex.Message });
            }
        }        
    }
}
