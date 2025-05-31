using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Model
{
    public class ResturantContext : DbContext
    {
        
        public DbSet<User> Users { get; set; }

        public ResturantContext(DbContextOptions options ) : base(options)
        {
            
        }
    }
}
