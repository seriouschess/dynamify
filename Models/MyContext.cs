using dynamify.Models.SiteModels;
using Microsoft.EntityFrameworkCore;

namespace dynamify.Models
{
    public class MyContext : DbContext
    {
        //base() calls the parent class' constructor passing the "opptions" parameter along public MyContext(DbContextOptions options) : base(options) { }
        public MyContext(DbContextOptions options) : base (options) { }

        public DbSet<Admin> Admins {get;set;}

        public DbSet<ParagraphBox>  ParagraphBoxes {get;set;}

        public DbSet<Site> Sites {get;set;}
    }
}