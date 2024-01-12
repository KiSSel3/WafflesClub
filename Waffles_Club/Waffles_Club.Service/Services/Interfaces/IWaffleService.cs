using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waffles_Club.Data.Entity;
using Waffles_Club.Data.Helpers;
using Waffles_Club.Data.ViewModels;

namespace Waffles_Club.Service.Services.Interfaces
{
	public interface IWaffleService
	{
		public Task<PaginatedList<Waffle>> GetWaffleListAsync(string? waffleTypeNormalizedName = null, string? fillingTypeNormalizedName = null, int pageNow = 1, int pageSize = 6);
		public Task<Waffle> GetWaffleByIdAsync(Guid waffleId);
		public Task<Waffle> DeleteWaffleAsync(Guid waffleId);
		public Task<Waffle> CreateWaffleAsync(WaffleViewModel viewModel);
		public Task<Waffle> UpdateWaffleAsync(Guid waffleId, WaffleViewModel viewModel);
	}
}
