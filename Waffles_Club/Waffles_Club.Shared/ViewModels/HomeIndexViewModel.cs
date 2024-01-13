using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Waffles_Club.Data.Entity;
using Waffles_Club.Shared.Helpers;

namespace Waffles_Club.Shared.ViewModels
{
	public class HomeIndexViewModel
	{
		public PaginatedList<Waffle> Waffles { get; set; }
		public SelectList SelectListWaffleType { get; set; }
		public SelectList SelectListFillingType { get; set; }
	}
}
