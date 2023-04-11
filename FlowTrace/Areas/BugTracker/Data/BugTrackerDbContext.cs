using FlowTrace.Areas.BugTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace FlowTrace.Areas.BugTracker.Data
{
    public class BugTrackerDbContext : DbContext
    {
        public BugTrackerDbContext(DbContextOptions<BugTrackerDbContext> options) : base(options)
        {

        }
        public DbSet<Bug> Bugs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
