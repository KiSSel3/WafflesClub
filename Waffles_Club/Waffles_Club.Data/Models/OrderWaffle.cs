namespace Waffles_Club.Data.Models;

public class OrderWaffle
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid WaffleId { get; set; }
}