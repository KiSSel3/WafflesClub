using Waffles_Club.Data.Enum;

namespace Waffles_Club.Data.Entity;

public class Order
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }
    public OrderStatus OrderStatus { get; set; }
}