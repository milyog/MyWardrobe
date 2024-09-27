using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MyWardrobe.Contracts.WardrobeItem;
using MyWardrobe.Data;
using MyWardrobe.Models;

namespace MyWardrobe.Services.WardrobeItems
{
    public class WardrobeItemService : IWardrobeItemService
    {
        private readonly MyWardrobeDbContext _context;        

        public WardrobeItemService(MyWardrobeDbContext context)
        {
            _context = context;
        }
        public void CreateWardrobeItem(WardrobeItem wardrobeItem)
        {
            _context.WardrobeItems.Add(wardrobeItem);
            _context.SaveChanges();
        }
        
        public List<WardrobeItem> GetWardrobeItems()
        {
            return _context.WardrobeItems
                .Include(x => x.WardrobeItemUsages.OrderBy(
                    x => x.WardrobeItemUsageDateTime))
                .ToList();
        }

        public WardrobeItem GetWardrobeItem(Guid id)
        {
            return _context.WardrobeItems
                .Include(x => x.WardrobeItemUsages.OrderBy(
                    x => x.WardrobeItemUsageDateTime))
                .FirstOrDefault(x => x.Id == id);
        }
                    
        public void UpdateWardrobeItem(WardrobeItem updatedWardrobeItem)
        {
            _context.WardrobeItems.Update(updatedWardrobeItem);
            _context.SaveChanges();
        }

        public void DeleteWardrobeItem(Guid id) 
        {
            var wardrobeItem = _context.WardrobeItems.Find(id);

            _context.WardrobeItems.Remove(wardrobeItem);
            _context.SaveChanges();
        }
    }
}
