using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waffles_Club.Shared.ViewModels
{
	public class TypeViewModel
	{
		[Required(ErrorMessage = "Необходимо заполнить поле Name")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Необходимо заполнить поле NormalizedName")]
		public string NormalizedName { get; set; }
	}
}
