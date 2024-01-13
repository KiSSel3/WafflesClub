using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waffles_Club.Data.Entity;
using Waffles_Club.Data.Helpers;
using Waffles_Club.Data.ViewModels;
using Waffles_Club.DataManagment.Interfaces;
using Waffles_Club.Service.Services.Interfaces;

namespace Waffles_Club.Service.Services.Implementations
{
	public class WaffleService : IWaffleService
	{
		private readonly IWaffleRepository _waffleRepository;

		public WaffleService(IWaffleRepository waffleRepository) =>
			_waffleRepository = waffleRepository;

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

		public async Task<PaginatedList<Waffle>> GetWaffleListAsync(string? waffleName = null, Guid? waffleTypeId = null, Guid? fillingTypeId = null, int pageNow = 1, int pageSize = 6)
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
				waffles = waffles.Where(w => w.Name.ToLower().Contains(waffleName.ToLower())).ToList();
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

			paginatedList.Items = waffles.Skip((pageNow - 1) * pageSize).Take(pageSize).ToList();
			paginatedList.CurrentPage = pageNow;
			paginatedList.TotalPages = totalPages;

			return paginatedList;
		}

		public async Task<Waffle> UpdateWaffleAsync(Guid waffleId, WaffleViewModel viewModel)
		{
			var waffleByName = await _waffleRepository.GetByName(viewModel.Name);
			if (waffleByName != null)
			{
				throw new Exception("A waffle already exists");
			}

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

			await _waffleRepository.Update(waffleById);

			return waffleById;
		}
	}
}
