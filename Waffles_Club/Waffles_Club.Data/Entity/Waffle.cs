namespace Waffles_Club.Data.Entity;

public class Waffle : BaseEntity
{
   public Guid TypeId { get; set; }
   public Guid FillingTypeId { get; set; }
   public string Name { get; set; }
   public string Description { get; set; }
   public string ImageUrl { get; set; }
   public int CountInPackage { get; set; }
   public decimal Price { get; set; }
   public double Weight { get; set; }
}