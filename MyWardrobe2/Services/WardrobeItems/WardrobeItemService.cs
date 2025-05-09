﻿using Microsoft.AspNetCore.Http.HttpResults;
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
        public void CreateWardrobeItem(WardrobeItem wardrobeItem)
        {
            _context.WardrobeItems.Add(wardrobeItem);
            _context.SaveChanges();
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

            //return _context.WardrobeItems
            //    .Include(x => x.WardrobeItemUsages.OrderBy(
            //        x => x.WardrobeItemUsageDateTime))
            //    .ToList();
        }

        public Result<WardrobeItem> GetWardrobeItem(Guid id)  //WardrobeItem
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

            //return _context.WardrobeItems
            //    .Include(x => x.WardrobeItemUsages.OrderBy(
            //        x => x.WardrobeItemUsageDateTime))
            //    .FirstOrDefault(x => x.Id == id);
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
