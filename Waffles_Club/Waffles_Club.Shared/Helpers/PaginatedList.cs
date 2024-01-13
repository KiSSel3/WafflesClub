using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waffles_Club.Shared.Helpers
{
	public class PaginatedList<T> where T : class
 	{
		public List<T> Items { get; set; } = new();
		public int CurrentPage { get; set; } = 1;
		public int TotalPages { get; set; } = 1;
	}
}
