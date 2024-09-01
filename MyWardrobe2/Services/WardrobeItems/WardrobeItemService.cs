using MyWardrobe.Data;
using MyWardrobe.Models;

namespace MyWardrobe.Services.WardrobeItems
{
    public class WardrobeItemService : IWardrobeItemService
    {
        private readonly DataContext _context;        

        public WardrobeItemService(DataContext context)
        {
            _context = context;
        }
        public void CreateWardrobeItem(WardrobeItem wardrobeItem)
        {
            _context.Add(wardrobeItem);
            _context.SaveChanges();
        }
        
        public List<WardrobeItem> GetWardrobeItems()
        {
            return _context.WardrobeItems.ToList();
        }

        public WardrobeItem GetWardrobeItem(Guid id)
        {
            return _context.WardrobeItems.Find(id);
        }
    }
}
