using Microsoft.EntityFrameworkCore;
using House_TARgv20.Core.Domain;

namespace House_TARgv20.Data
{
    public class HouseDbContext : DbContext
    {
        public HouseDbContext(DbContextOptions<HouseDbContext> options)
            : base(options) { }

        public DbSet<HouseDomain> House { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HouseDomain>(entity =>
            {
                entity.HasKey(x => x.Id);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
