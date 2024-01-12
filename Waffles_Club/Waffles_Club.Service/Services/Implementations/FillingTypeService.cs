using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waffles_Club.Data.Entity;
using Waffles_Club.Data.ViewModels;
using Waffles_Club.DataManagment.Implementations;
using Waffles_Club.DataManagment.Interfaces;
using Waffles_Club.Service.Services.Interfaces;

namespace Waffles_Club.Service.Services.Implementations
{
	public class FillingTypeService : IFillingTypeService
	{
		private readonly IFillingTypeRepository _fillingTypeRepository;

		public FillingTypeService(IFillingTypeRepository fillingTypeRepository) =>
			_fillingTypeRepository = fillingTypeRepository;

		public async Task<FillingType> CreateAsync(TypeViewModel viewModel)
		{
			var fillingTypeByNormalizedName = await _fillingTypeRepository.GetByNormalizedName(viewModel.NormalizedName);

			if (fillingTypeByNormalizedName != null)
			{
				throw new Exception("A filling type already exists");
			}

			var newFillingType = new FillingType
			{
				Name = viewModel.Name,
				NormalizedName = viewModel.NormalizedName
			};

			await _fillingTypeRepository.Create(newFillingType);

			return newFillingType;
		}

		public async Task<FillingType> DeleteAsync(Guid fillingTypeId)
		{
			var fillingTypeById = await _fillingTypeRepository.GetById(fillingTypeId);
			if (fillingTypeById == null)
			{
				throw new Exception("No filling type");
			}

			await _fillingTypeRepository.Delete(fillingTypeById);

			return fillingTypeById;
		}

		public async Task<List<FillingType>> GetAllAsync()
		{
			var fillingTypes = await _fillingTypeRepository.GetAll();

			return fillingTypes;
		}

		public async Task<FillingType> GetById(Guid fillingTypeId)
		{
			var fillingTypeById = await _fillingTypeRepository.GetById(fillingTypeId);
			if (fillingTypeById == null)
			{
				throw new Exception("No filling type");
			}

			return fillingTypeById;
		}

		public async Task<FillingType> UpdateAsync(Guid fillingTypeId, TypeViewModel viewModel)
		{
			var fillingTypeByNormalizedName = await _fillingTypeRepository.GetByNormalizedName(viewModel.NormalizedName);

			if (fillingTypeByNormalizedName != null)
			{
				throw new Exception("A filling type already exists");
			}

			var fillingTypeById = await _fillingTypeRepository.GetById(fillingTypeId);
			if (fillingTypeById == null)
			{
				throw new Exception("No filling type");
			}

			fillingTypeById.Name = viewModel.Name;
			fillingTypeById.NormalizedName = viewModel.NormalizedName;

			await _fillingTypeRepository.Update(fillingTypeById);

			return fillingTypeById;
		}
	}
}
