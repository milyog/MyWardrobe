using MyWardrobe.Contracts.WardrobeItem;
using MyWardrobe.ErrorHandling;
using MyWardrobe.Models;

namespace MyWardrobe.Services.WardrobeItems
{
    public interface IWardrobeItemService
    {
        Result<WardrobeItem> CreateWardrobeItem(WardrobeItem wardrobeItem);
        Result<List<WardrobeItem>> GetWardrobeItems();
        Result<WardrobeItem> GetWardrobeItem(Guid id);
        Result<WardrobeItem> UpdateWardrobeItem(Guid id, WardrobeItem wardrobeItem);
        Result DeleteWardrobeItem(Guid id);
    }
}
