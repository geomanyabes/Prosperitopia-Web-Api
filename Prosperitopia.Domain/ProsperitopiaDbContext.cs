using Microsoft.EntityFrameworkCore;
using Prosperitopia.Domain.Model.Entity;

namespace Prosperitopia.Domain
{
    public class ProsperitopiaDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ProsperitopiaDbContext()
        {

        }
        public ProsperitopiaDbContext(DbContextOptions<ProsperitopiaDbContext> options) : base(options) 
        { 

        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


        /// <summary>
        /// Comment out for production.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Prosperitopia");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Item>(entity =>
            {
                entity.ToTable("Item");
                entity.HasOne(e => e.Category)
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
