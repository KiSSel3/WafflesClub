namespace Waffles_Club.Shared.ViewModels;

public class CartViewModel
{
    public string UserId { get; set; }
    public Guid WaffleId { get; set; }
    public int Count { get; set; }
}