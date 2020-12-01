using dynamify.Models.AnalyticsModels;
using dynamify.Models.DataPlans;
using dynamify.Models.SiteModels;
using Microsoft.EntityFrameworkCore;

namespace dynamify.Models
{
    public class DatabaseContext : DbContext
    {
        //base() calls the parent class' constructor passing the "opptions" parameter along public DatabaseContext(DbContextOptions options) : base(options) { }
        public DatabaseContext(DbContextOptions options) : base (options) { }

        public DbSet<Admin> Admins {get;set;}

        public DbSet<Site> Sites {get;set;}

        public DbSet<DataPlan> DataPlans {get;set;}

        //site components
        public DbSet<ParagraphBox>  ParagraphBoxes {get;set;}

        public DbSet<Image> Images {get;set;}

        public DbSet<Portrait> Portraits {get;set;}

        public DbSet<TwoColumnBox> TwoColumnBoxes {get;set;}

        public DbSet<LinkBox> LinkBoxes {get;set;}

        public DbSet<NavLink> NavLinks {get;set;}

        public DbSet<NavBar> NavBars {get;set;}

        //analytics
        public DbSet<ViewSession> ViewSessions {get;set;}
    }
}