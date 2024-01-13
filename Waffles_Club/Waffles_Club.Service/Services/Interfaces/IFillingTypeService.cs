using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waffles_Club.Data.Entity;
using Waffles_Club.Data.ViewModels;

namespace Waffles_Club.Service.Services.Interfaces
{
	public interface IFillingTypeService
	{
		public Task<List<FillingType>> GetAllAsync();
		public Task<FillingType> CreateAsync(TypeViewModel viewModel);
		public Task<FillingType> UpdateAsync(Guid fillingTypeId, TypeViewModel viewModel);
		public Task<FillingType> DeleteAsync(Guid fillingTypeId);
		public Task<FillingType> GetByIdAsync(Guid fillingTypeId);
	}
}
