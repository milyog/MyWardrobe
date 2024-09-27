using Microsoft.EntityFrameworkCore;
using MyWardrobe.Contracts.WardrobeItem;
using MyWardrobe.Data;
using MyWardrobe.Models;
using MyWardrobe.Services.WardrobeItems;

namespace MyWardrobe.Services.WardrobeItemsUsage
{
    public class WardrobeItemUsageService : IWardrobeItemUsageService
    {
        private readonly MyWardrobeDbContext _context;
        private readonly IWardrobeItemService _wardrobeItemService;

        public WardrobeItemUsageService(MyWardrobeDbContext context, IWardrobeItemService wardrobeItemService) 
        {
            _context = context;
            _wardrobeItemService = wardrobeItemService;
        } 
        public WardrobeItemUsage CreateWardrobeItemUsage(Guid id)
        {
            var wardrobeItem = _wardrobeItemService.GetWardrobeItem(id);

            int updatedWardrobeItemUsageCounter = UpdateWardrobeItemUsageCounter(wardrobeItem);
               
            var newWardrobeItemUsage = new WardrobeItemUsage
                {
                    Id = Guid.NewGuid(),
                    WardrobeItemUsageCounter = updatedWardrobeItemUsageCounter,
                    WardrobeItemUsageDateTime = DateTime.Now,
                    WardrobeItemId = id
                };

            _context.WardrobeItemsUsage.Add(newWardrobeItemUsage);
            _context.SaveChanges();

            return newWardrobeItemUsage;
        }

        public WardrobeItemUsage GetWardrobeItemUsage(Guid id)
        {
            return _context.WardrobeItemsUsage
                .Include(x => x.WardrobeItem) 
                .FirstOrDefault(x => x.Id == id);
        }



        private static int UpdateWardrobeItemUsageCounter(WardrobeItem WardrobeItem)
        {
            var wardrobeItemUsageCounter = WardrobeItem.WardrobeItemUsages     
                .AsEnumerable()
                .OrderByDescending(x => x.WardrobeItemUsageDateTime)                                                           
                .Select(x => x.WardrobeItemUsageCounter)
                .FirstOrDefault();

            var updatedWardrobeItemUsageCounter = wardrobeItemUsageCounter + 1;

            return updatedWardrobeItemUsageCounter;
        }
    }
}
