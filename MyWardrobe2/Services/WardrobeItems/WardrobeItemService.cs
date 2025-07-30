using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MyWardrobe.Contracts.WardrobeItem;
using MyWardrobe.Data;
using MyWardrobe.ErrorHandling;
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
        public async Task<Result<WardrobeItem>> CreateWardrobeItem(WardrobeItem wardrobeItem)
        {
            await _context.WardrobeItems.AddAsync(wardrobeItem);
            await _context.SaveChangesAsync();

            // Failure saknas förnärvarande (jfr conrollern).
            return Result<WardrobeItem>.Success(wardrobeItem);
        }

        public async Task<Result<List<WardrobeItem>>> GetWardrobeItems()
        {
            var result = await _context.WardrobeItems
                .Include(x => x.WardrobeItemUsages
                .OrderBy(x => x.WardrobeItemUsageDateTime))
                .ToListAsync();

            if (result.Count == 0)
            {
                return Result<List<WardrobeItem>>.Failure(WardrobeItemErrors.NotFound());
            }

            return Result<List<WardrobeItem>>.Success(result);
        }

        public async Task<Result<WardrobeItem>> GetWardrobeItem(Guid id) 
        {
            var result = await _context.WardrobeItems
               .Include(x => x.WardrobeItemUsages
               .OrderBy(x => x.WardrobeItemUsageDateTime))
               .FirstOrDefaultAsync(x => x.Id == id);

            if (result is null)
            {
                return Result<WardrobeItem>.Failure(WardrobeItemErrors.NotFound(id));
            }

            return Result<WardrobeItem>.Success(result);
        }

        public async Task<Result<WardrobeItem>> UpdateWardrobeItem(Guid id, WardrobeItem updatedWardrobeItem)
        {
            var result = await _context.WardrobeItems.FirstOrDefaultAsync(x => x.Id == id);

            if (result is null)
            {
                return Result<WardrobeItem>.Failure(WardrobeItemErrors.NotFound(id));
            }

            _context.WardrobeItems.Update(updatedWardrobeItem);
            await _context.SaveChangesAsync();

            return Result<WardrobeItem>.Success(result);
        }

        public async Task<Result> DeleteWardrobeItem(Guid id)
        {
            var wardrobeItem = await _context.WardrobeItems.FirstOrDefaultAsync(x => x.Id == id);

            if (wardrobeItem is null)
            {
                return Result<WardrobeItem>.Failure(WardrobeItemErrors.NotFound(id));
            }

            _context.WardrobeItems.Remove(wardrobeItem);
            await _context.SaveChangesAsync();

            return Result.Success();
        }
    }
}
