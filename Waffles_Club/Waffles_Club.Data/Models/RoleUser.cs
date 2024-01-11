namespace Waffles_Club.Data.Models;

public class RoleUser
{
    public Guid Id { get; set; }
    public Guid RoleId { get; set; }
    public Guid UserId { get; set; }

}