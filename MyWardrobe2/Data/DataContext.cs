using Microsoft.EntityFrameworkCore;
using MyWardrobe.Models;

namespace MyWardrobe.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { 
            
        }

        public DbSet<WardrobeItem> WardrobeItems { get; set; }
    }
}