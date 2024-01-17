using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waffles_Club.Data.Entity;
using Waffles_Club.Shared.Helpers;
using Waffles_Club.Shared.ViewModels;
using Waffles_Club.DataManagment.Interfaces;
using Waffles_Club.Service.Services.Interfaces;
using Waffles_Club.Data.Enum;

namespace Waffles_Club.Service.Services.Implementations
{
	public class WaffleService : IWaffleService
	{
		private readonly IWaffleRepository _waffleRepository;
		private readonly IWaffleTypeRepository _waffleTypeRepository;
		private readonly IFillingTypeRepository _fillingTypeRepository;

		public WaffleService(IWaffleRepository waffleRepository, IFillingTypeRepository fillingTypeRepository, IWaffleTypeRepository waffleTypeRepository)
		{
			_waffleRepository = waffleRepository;
			_fillingTypeRepository = fillingTypeRepository;
			_waffleTypeRepository= waffleTypeRepository;
		}

        public async Task<Waffle> CreateWaffleAsync(WaffleViewModel viewModel)
		{
			var waffleByName = await _waffleRepository.GetByName(viewModel.Name);
			if(waffleByName != null)
			{
				throw new Exception("A waffle already exists");
			}

			var newWaffle = new Waffle
			{
				TypeId = viewModel.TypeId,
				FillingTypeId = viewModel.FillingTypeId,
				Name = viewModel.Name,
				Description = viewModel.Description,
				ImageUrl = viewModel.ImageUrl,
				CountInPackage = viewModel.CountInPackage,
				Price = viewModel.Price
			};

			await _waffleRepository.Create(newWaffle);

			return newWaffle;
		}

		public async Task<Waffle> DeleteWaffleAsync(Guid waffleId)
		{
			var waffleById = await _waffleRepository.GetById(waffleId);
			if(waffleById == null)
			{
				throw new Exception("No waffle");
			}

			await _waffleRepository.Delete(waffleById);

			return waffleById;
		}

		public async Task<Waffle> GetWaffleByIdAsync(Guid waffleId)
		{
			var waffleById = await _waffleRepository.GetById(waffleId);
			if (waffleById == null)
			{
				throw new Exception("No waffle");
			}

			return waffleById;
		}
		public async Task<WaffleDetailsViewModel> GetWaffleDetailById(Guid waffleId)
		{
            var waffleById = await _waffleRepository.GetById(waffleId);
            if (waffleById == null)
            {
                throw new Exception("No waffle");
            }
            var waffleType = (await _waffleTypeRepository.GetAll()).FirstOrDefault(a=>a.Id==waffleById.TypeId);
            var fillingType = (await _fillingTypeRepository.GetAll()).FirstOrDefault(a => a.Id == waffleById.FillingTypeId);
			var waffleDetail = new WaffleDetailsViewModel()
			{
				FillingType = fillingType,
				WaffleType = waffleType,
				Waffle = waffleById
			};
			return waffleDetail;
        }

        public async Task<List<WaffleDetailsViewModel>> GetWaffleDetailsList()
        {
			var waffles = await _waffleRepository.GetAll();
            var waffleTypes = await _waffleTypeRepository.GetAll();
            var fillingTypes = await _fillingTypeRepository.GetAll();

            // Преобразовываем каждый объект Waffle в WaffleDetailsViewModel
            var waffleDetailsList = waffles.Select(waffle => new WaffleDetailsViewModel
            {
                Waffle = waffle,
                WaffleType = waffleTypes.FirstOrDefault(type => type.Id == waffle.TypeId),
                FillingType = fillingTypes.FirstOrDefault(type => type.Id == waffle.FillingTypeId)
            }).ToList();

            return waffleDetailsList;
        }


        public async Task<PaginatedList<Waffle>> GetWaffleListAsync(string? waffleName = null, Guid? waffleTypeId = null, Guid? fillingTypeId = null,
																	decimal? minPrice = 0, decimal? maxPrice = decimal.MaxValue, int pageNow = 1, int pageSize = 6, SortingParameters sortingParameters = SortingParameters.None)
		{
			var paginatedList = new PaginatedList<Waffle>();

			var waffles = await _waffleRepository.GetAll();

			if(waffleTypeId != null)
			{
				waffles = waffles.Where(w => w.TypeId.Equals(waffleTypeId)).ToList();
			}

			if(fillingTypeId != null)
			{
				waffles = waffles.Where(w => w.FillingTypeId.Equals(fillingTypeId)).ToList();
			}

			if(waffleName != null)
			{
				waffles = waffles.Where(w => w.Name.IndexOf(waffleName, StringComparison.OrdinalIgnoreCase) != -1).ToList();
            }

			if(minPrice != null)
			{
				waffles = waffles.Where(w => w.Price >= minPrice).ToList();
            }

            if (maxPrice != null)
            {
                waffles = waffles.Where(w => w.Price <= maxPrice).ToList();
            }

            var countWaffles = waffles.Count;

			if(countWaffles == 0)
			{
				return paginatedList;
			}

			int totalPages = (int) Math.Ceiling(countWaffles / (double)pageSize);

			if(pageNow > totalPages)
			{
				throw new Exception("No such page");
			}

			waffles = Sorting(waffles, sortingParameters);

            paginatedList.Items = waffles.Skip((pageNow - 1) * pageSize).Take(pageSize).ToList();
			paginatedList.CurrentPage = pageNow;
			paginatedList.TotalPages = totalPages;

			return paginatedList;
		}

		public async Task<Waffle> UpdateWaffleAsync(Guid waffleId, WaffleViewModel viewModel)
		{
			
			var waffleById = await _waffleRepository.GetById(waffleId);
			if (waffleById == null)
			{
				throw new Exception("No waffle");
			}

			waffleById.TypeId = viewModel.TypeId;
			waffleById.FillingTypeId = viewModel.FillingTypeId;
			waffleById.Name = viewModel.Name;
			waffleById.Description = viewModel.Description;
			waffleById.ImageUrl = viewModel.ImageUrl;
			waffleById.CountInPackage = viewModel.CountInPackage;
			waffleById.Price = viewModel.Price;
			waffleById.Weight = viewModel.Weight;

			await _waffleRepository.Update(waffleById);

			return waffleById;
		}

		private List<Waffle> Sorting(List<Waffle> waffles, SortingParameters sortingParameters)
		{
			switch (sortingParameters)
			{
				case SortingParameters.PriceDecrease:
					return waffles.OrderByDescending(w => w.Price).ToList();

                case SortingParameters.PriceIncrease:
                    return waffles.OrderBy(w => w.Price).ToList();

                case SortingParameters.CountDecrease:
                    return waffles.OrderByDescending(w => w.CountInPackage).ToList();

                case SortingParameters.CountIncrease:
                    return waffles.OrderBy(w => w.CountInPackage).ToList();

                case SortingParameters.NameDecrease:
                    return waffles.OrderByDescending(w => w.Name).ToList();

                case SortingParameters.NameIncrease:
                    return waffles.OrderBy(w => w.Name).ToList();

                case SortingParameters.WeightDecrease:
                    return waffles.OrderByDescending(w => w.Weight).ToList();

                case SortingParameters.WeightIncrease:
                    return waffles.OrderBy(w => w.Weight).ToList();
            }

			return waffles;
        }
	}
}
