using Microsoft.EntityFrameworkCore;
using MyWardrobe.Models;

namespace MyWardrobe.Data
{
    public class MyWardrobeDbContext : DbContext
    {
        public MyWardrobeDbContext(DbContextOptions<MyWardrobeDbContext> options) : base(options)
        { 
            
        }

        public DbSet<WardrobeItem> WardrobeItems { get; set; }
        public DbSet<WardrobeItemUsage> WardrobeItemsUsage { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<WardrobeItemUsage>()
            //    .HasOne(x => x.WardrobeItem)
            //    .WithMany(x => x.WardrobeItemUsages)
            //    .HasForeignKey(x => x.WardrobeItemId);

            modelBuilder.Entity<WardrobeItem>()
                .HasMany(x => x.WardrobeItemUsages)
                .WithOne(x => x.WardrobeItem)
                .HasForeignKey(x => x.WardrobeItemId);

            //base.OnModelCreating(modelBuilder);
        }
    }
}