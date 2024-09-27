using MyWardrobe.Contracts.WardrobeItem;
using MyWardrobe.Models;

namespace MyWardrobe.Services.WardrobeItemsUsage
{
    public interface IWardrobeItemUsageService
    {
        WardrobeItemUsage CreateWardrobeItemUsage(Guid id);
        WardrobeItemUsage GetWardrobeItemUsage(Guid id);
    }
}
