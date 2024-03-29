﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waffles_Club.Data.Entity;
using Waffles_Club.Data.Enum;
using Waffles_Club.Shared.Helpers;
using Waffles_Club.Shared.ViewModels;

namespace Waffles_Club.Service.Services.Interfaces
{
	public interface IWaffleService
	{
		public Task<PaginatedList<Waffle>> GetWaffleListAsync(string? waffleName = null, Guid? waffleTypeId = null, Guid? fillingTypeId = null,
                                                              decimal? minPrice = 0, decimal? maxPrice = decimal.MaxValue, int pageNow = 1, int pageSize = 6, SortingParameters sortingParameters = SortingParameters.None);
		public Task<Waffle> GetWaffleByIdAsync(Guid waffleId);
		public Task<Waffle> DeleteWaffleAsync(Guid waffleId);
		public Task<Waffle> CreateWaffleAsync(WaffleViewModel viewModel);
		public Task<Waffle> UpdateWaffleAsync(Guid waffleId, WaffleViewModel viewModel);
		Task<List<WaffleDetailsViewModel>> GetWaffleDetailsList();
		Task<WaffleDetailsViewModel> GetWaffleDetailById(Guid waffleId);
    }
}
