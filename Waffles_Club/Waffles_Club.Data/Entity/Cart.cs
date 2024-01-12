namespace Waffles_Club.Data.Entity;

public class Cart : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid WaffleId { get; set; }
    public int Waffle { get; set; }
}