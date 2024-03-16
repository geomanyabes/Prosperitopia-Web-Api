using Microsoft.EntityFrameworkCore;
using Prosperitopia.Domain.Model.Entity;

namespace Prosperitopia.Domain
{
    public class ProsperitopiaDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public ProsperitopiaDbContext(DbContextOptions<ProsperitopiaDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Item>(e =>
            {
                e.ToTable("Item");
                e.HasOne(e => e.Category)
                .WithMany(e => e.Items)
                .HasForeignKey(e => e.CategoryId)
                .HasConstraintName("FK_Item_Category");
            });
            mb.Entity<Category>(e =>
            {
                e.ToTable("Category");

            });
        }
    }
}
