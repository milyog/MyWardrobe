using MyWardrobe.Contracts.WardrobeItem;
using MyWardrobe.Models;

namespace MyWardrobe.Services.WardrobeItemsUsage
{
    public interface IWardrobeItemUsageService
    {
        Task<WardrobeItemUsage> CreateWardrobeItemUsage(Guid id);
        Task<WardrobeItemUsage> GetWardrobeItemUsage(Guid id);
    }
}
