namespace Waffles_Club.Data.Entity;

public class Cart
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid WaffleId { get; set; }
}