using MyWardrobe.Contracts.WardrobeItem;
using MyWardrobe.ErrorHandling;
using MyWardrobe.Models;

namespace MyWardrobe.Services.WardrobeItemsUsage
{
    public interface IWardrobeItemUsageService
    {
        Task<Result<WardrobeItemUsage>> CreateWardrobeItemUsage(Guid id);
        Task<Result<WardrobeItemUsage>> GetWardrobeItemUsage(Guid id);
    }
}
