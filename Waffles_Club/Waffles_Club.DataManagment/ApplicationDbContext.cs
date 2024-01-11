using Microsoft.EntityFrameworkCore;
using Waffles_Club.Data.Entity;
using Waffles_Club.Data.Models;

namespace Waffles_Club.DataManagment;

public class ApplicationDbContext:DbContext
{
    public DbSet<Waffle> Waffles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<WaffleType> WaffleTypes { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<FillingType> FillingTypes { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<OrderWaffle> OrderWaffles { get; set; }
    public DbSet<RoleUser> RoleUsers { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
    
}