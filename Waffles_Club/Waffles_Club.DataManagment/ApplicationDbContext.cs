using Microsoft.EntityFrameworkCore;

namespace Waffles_Club.DataManagment;

public class ApplicationDbContext:DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }
    
}