using System.ComponentModel.DataAnnotations;

namespace Waffles_Club.Shared.ViewModels;

public class UpdateUserViewModel
{
    [MinLength(4, ErrorMessage = "Слишком короткий логин: минимальная длина 4 символа")]
    [Required(ErrorMessage = "Необходимо заполнить поле для логина")]
    public string Login { get; set; }

    [Required(ErrorMessage = "Необходимо заполнить поле для имя")]
    public string Name { get; set; }

    [EmailAddress]
    [Required(ErrorMessage = "Необходимо заполнить поле для почты")]
    public string Email { get; set; }
}