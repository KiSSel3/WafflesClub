using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waffles_Club.Data.Entity;
using Waffles_Club.Data.ViewModels;
using Waffles_Club.DataManagment.Interfaces;
using Waffles_Club.Service.Services.Interfaces;

namespace Waffles_Club.Service.Services.Implementations
{
	public class WaffleTypeService : IWaffleTypeService
	{
		private readonly IWaffleTypeRepository _waffleTypeRepository;

		public WaffleTypeService(IWaffleTypeRepository waffleTypeRepository) =>
			_waffleTypeRepository = waffleTypeRepository;

		public async Task<WaffleType> CreateAsync(TypeViewModel viewModel)
		{
			var waffleTypeByNormalizedName = await _waffleTypeRepository.GetByNormalizedName(viewModel.NormalizedName);

			if(waffleTypeByNormalizedName != null)
			{
				throw new Exception("A waffle type already exists");
			}

			var newWaffleType = new WaffleType
			{
				Name = viewModel.Name,
				NormalizedName = viewModel.NormalizedName
			};

			await _waffleTypeRepository.Create(newWaffleType);

			return newWaffleType;
		}

		public async Task<WaffleType> DeleteAsync(Guid waffleTypeId)
		{
			var waffleTypeById = await _waffleTypeRepository.GetById(waffleTypeId);
			if(waffleTypeById == null)
			{
				throw new Exception("No waffle type");
			}

			await _waffleTypeRepository.Delete(waffleTypeById);

			return waffleTypeById;
		}

		public async Task<List<WaffleType>> GetAllAsync()
		{
			var waffleTypes = await _waffleTypeRepository.GetAll();

			return waffleTypes;
		}

		public async Task<WaffleType> UpdateAsync(Guid waffleTypeId, TypeViewModel viewModel)
		{
			var waffleTypeByNormalizedName = await _waffleTypeRepository.GetByNormalizedName(viewModel.NormalizedName);

			if (waffleTypeByNormalizedName != null)
			{
				throw new Exception("A waffle type already exists");
			}

			var waffleTypeById = await _waffleTypeRepository.GetById(waffleTypeId);
			if (waffleTypeById == null)
			{
				throw new Exception("No waffle type");
			}

			waffleTypeById.Name = viewModel.Name;
			waffleTypeById.NormalizedName = viewModel.NormalizedName;

			await _waffleTypeRepository.Update(waffleTypeById);

			return waffleTypeById;
		}
	}
}
