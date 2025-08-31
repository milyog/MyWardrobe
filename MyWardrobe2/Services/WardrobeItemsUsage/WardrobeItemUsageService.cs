using Microsoft.EntityFrameworkCore;
using MyWardrobe.Contracts.WardrobeItem;
using MyWardrobe.Data;
using MyWardrobe.ErrorHandling;
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
        public async Task<Result<WardrobeItemUsage>> CreateWardrobeItemUsage(Guid id)
        {
            var wardrobeItem = await _wardrobeItemService.GetWardrobeItem(id);

            int updatedWardrobeItemUsageCounter = UpdateWardrobeItemUsageCounter(wardrobeItem.Value);
               
            var newWardrobeItemUsage = new WardrobeItemUsage
                {
                    Id = Guid.NewGuid(),
                    WardrobeItemUsageCounter = updatedWardrobeItemUsageCounter,
                    WardrobeItemUsageDateTime = DateTime.Now,
                    WardrobeItemId = id
                };

            await _context.WardrobeItemsUsage.AddAsync(newWardrobeItemUsage);
            await _context.SaveChangesAsync();

            // Failure saknas förnärvarande (jfr conrollern).
            return Result<WardrobeItemUsage>.Success(newWardrobeItemUsage);
        }

        public async Task<Result<WardrobeItemUsage>> GetWardrobeItemUsage(Guid id)
        {
            var result = await _context.WardrobeItemsUsage
                .Include(x => x.WardrobeItem)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (result is null)
            {
                return Result<WardrobeItemUsage>.Failure(WardrobeItemErrors.NotFound(id));
            }

            return Result<WardrobeItemUsage>.Success(result);
        }



        private static int UpdateWardrobeItemUsageCounter(WardrobeItem wardrobeItem)
        {
            var wardrobeItemUsageCounter = wardrobeItem.WardrobeItemUsages     
                .AsEnumerable()
                .OrderByDescending(x => x.WardrobeItemUsageDateTime)                                                           
                .Select(x => x.WardrobeItemUsageCounter)
                .FirstOrDefault();

            var updatedWardrobeItemUsageCounter = wardrobeItemUsageCounter + 1;

            return updatedWardrobeItemUsageCounter;
        }
    }
}
