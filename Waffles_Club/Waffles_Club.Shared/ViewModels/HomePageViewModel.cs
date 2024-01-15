using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Waffles_Club.Data.Entity;
using Waffles_Club.Shared.Helpers;

namespace Waffles_Club.Shared.ViewModels
{
	public class HomePageViewModel
	{
        public PaginatedList<Waffle> Waffles { get; set; }
		public List<WaffleType> WaffleTypes { get; set; }
		public List<FillingType> FillingTypes { get; set; }

		public string? CurrentWaffleName { get; set; }

		public Guid? CurrentWaffleTypeId { get; set; }
		public string CurrentWaffleTypeName { get; set; }

		public Guid? CurrentFillingTypeId { get; set; }
		public string CurrentFillingTypeName { get; set; }

		public decimal? CurrentMinPrice { get; set; }
		public decimal? CurrentMaxPrice { get; set; }
	}
}
