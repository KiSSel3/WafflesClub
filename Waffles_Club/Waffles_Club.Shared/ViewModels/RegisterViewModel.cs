using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waffles_Club.Shared.ViewModels
{
	public class RegisterViewModel
	{
		[MinLength(4, ErrorMessage = "Слишком короткий логин: минимальная длина 4 символа")]
		[Required(ErrorMessage = "Необходимо заполнить поле для логина")]
		public string Login { get; set; }

		[Required(ErrorMessage = "Необходимо заполнить поле для имя")]
		public string Name { get; set; }

		[MinLength(5, ErrorMessage = "Слишком короткий пароль: минимальная длина 5 символов")]
		[Required(ErrorMessage = "Необходимо заполнить поле для пароля")]
		public string Password { get; set; }

		[EmailAddress]
		[Required(ErrorMessage = "Необходимо заполнить поле для почты")]
		public string Email { get; set; }
	}
}
