using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waffles_Club.Shared.ViewModels
{
	public class WaffleViewModel
	{
		[Required(ErrorMessage = "Выберите тип")]
		public Guid TypeId { get; set; }

		[Required(ErrorMessage = "Выберите начинку")]
		public Guid FillingTypeId { get; set; }

		[Required(ErrorMessage = "Необходимо заполнить поле для имя")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Необходимо заполнить описание")]
		public string Description { get; set; }

		[Required(ErrorMessage = "Необходимо указать url изображения")]
		public string ImageUrl { get; set; }

		[Required(ErrorMessage = "Необходимо указать количество в упаковке")]
		public int CountInPackage { get; set; }

		[Required(ErrorMessage = "Необходимо указать цену")]
		[Range(0.01, double.MaxValue, ErrorMessage = "Цена должна быть больше 0.01 $")]
		public decimal Price { get; set; }
	}
}
