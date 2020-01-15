using Microsoft.EntityFrameworkCore;

namespace dynamify.Models
{
    public class SiteContext : DbContext
    {
        //base() calls the parent class' constructor passing the "opptions" parameter along public MyContext(DbContextOptions options) : base(options) { }
        public SiteContext(DbContextOptions options) : base (options) { }

        public DbSet<Admin> Admins {get;set;} 
    }
}