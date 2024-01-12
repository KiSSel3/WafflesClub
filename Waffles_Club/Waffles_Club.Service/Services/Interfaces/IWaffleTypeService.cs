using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waffles_Club.Data.Entity;
using Waffles_Club.Data.ViewModels;

namespace Waffles_Club.Service.Services.Interfaces
{
	public interface IWaffleTypeService
	{
		public Task<List<WaffleType>> GetAllAsync();
		public Task<WaffleType> CreateAsync(TypeViewModel viewModel);
		public Task<WaffleType> UpdateAsync(Guid waffleTypeId, TypeViewModel viewModel);
		public Task<WaffleType> DeleteAsync(Guid waffleTypeId);
		public Task<WaffleType> GetById(Guid waffleTypeId);
	}
}
