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
        public Result<WardrobeItem> CreateWardrobeItem(WardrobeItem wardrobeItem)
        {
            _context.WardrobeItems.Add(wardrobeItem);
            _context.SaveChanges();

            // Failure saknas förnärvarande (jfr conrollern).
            return Result<WardrobeItem>.Success(wardrobeItem);
        }
        
        public Result<List<WardrobeItem>> GetWardrobeItems()
        {
            var result = _context.WardrobeItems
                .Include(x => x.WardrobeItemUsages.OrderBy(
                    x => x.WardrobeItemUsageDateTime))
                .ToList();

            if (result.Count == 0)
            {
                return Result<List<WardrobeItem>>.Failure(WardrobeItemErrors.NotFound());
            }

            return Result<List<WardrobeItem>>.Success(result);
        }

        public Result<WardrobeItem> GetWardrobeItem(Guid id)
        {
            var result = _context.WardrobeItems
               .Include(x => x.WardrobeItemUsages.OrderBy(
                   x => x.WardrobeItemUsageDateTime))
               .FirstOrDefault(x => x.Id == id);

            if (result is null)
            {
                return Result<WardrobeItem>.Failure(WardrobeItemErrors.NotFound(id));
            }

            return Result<WardrobeItem>.Success(result);
        }
                    
        public Result<WardrobeItem> UpdateWardrobeItem(Guid id, WardrobeItem updatedWardrobeItem)
        {
            var result = _context.WardrobeItems.FirstOrDefault(x => x.Id == id);

            if (result is null)
            {
                return Result<WardrobeItem>.Failure(WardrobeItemErrors.NotFound(id));
            }
             
            _context.WardrobeItems.Update(updatedWardrobeItem);
            _context.SaveChanges();

            return Result<WardrobeItem>.Success(result);
        }

        public Result DeleteWardrobeItem(Guid id) 
        {
            var wardrobeItem = _context.WardrobeItems.FirstOrDefault(x => x.Id == id);

            if (wardrobeItem is null)
            {
                return Result<WardrobeItem>.Failure(WardrobeItemErrors.NotFound(id));
            }

            _context.WardrobeItems.Remove(wardrobeItem);
            _context.SaveChanges();

            return Result.Success();
        }
    }
}
