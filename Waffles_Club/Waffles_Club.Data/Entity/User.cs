using System.ComponentModel.DataAnnotations;

namespace Waffles_Club.Data.Entity;

public class User : BaseEntity
{
    [MinLength(4)]
    [Required]
    public string Login { get; set; }
    public string Name { get; set; }
    [MinLength(5)]
    [Required]
    public string Password { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}