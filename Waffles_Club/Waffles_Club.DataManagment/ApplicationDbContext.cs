using Microsoft.EntityFrameworkCore;
using Waffles_Club.Data.Entity;
using Waffles_Club.Data.Models;

namespace Waffles_Club.DataManagment;

public class ApplicationDbContext : DbContext
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
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Role>().HasData(new Role
        {
            Id = new Guid("44546e06-8719-4ad8-b88a-f271ae9d6eab"),
            Name = "User"
        });
        modelBuilder.Entity<Role>().HasData(new Role
        {
            Id = new Guid("9E7BF79C-BF94-4B40-A4F6-A3660E9DFD4A"),
            Name = "Admin"
        });
        modelBuilder.Entity<User>().HasData(new User()
        {
            Email = "alsemkovbn@gmail.com",
            Id = new Guid("DB571F33-8399-4B5D-A210-07FF85B784EE"),
            Login = "Admin",
            Name = "Admin",
            Password = "123498Admin"
        });
        modelBuilder.Entity<RoleUser>().HasData(new RoleUser()
        {
            RoleId = new Guid("9E7BF79C-BF94-4B40-A4F6-A3660E9DFD4A"),
            UserId = new Guid("DB571F33-8399-4B5D-A210-07FF85B784EE"),
            Id = Guid.NewGuid()
        });
    }
}