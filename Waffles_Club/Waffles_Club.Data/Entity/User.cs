using System.ComponentModel.DataAnnotations;

namespace Waffles_Club.Data.Entity;

public class User : BaseEntity
{
    public string Login { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}