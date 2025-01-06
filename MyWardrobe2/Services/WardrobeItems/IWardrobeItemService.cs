using MyWardrobe.Contracts.WardrobeItem;
using MyWardrobe.ErrorHandling;
using MyWardrobe.Models;

namespace MyWardrobe.Services.WardrobeItems
{
    public interface IWardrobeItemService
    {
        void CreateWardrobeItem(WardrobeItem wardrobeItem);
        List<WardrobeItem> GetWardrobeItems();
        Result<WardrobeItem> GetWardrobeItem(Guid id);
        void UpdateWardrobeItem(WardrobeItem wardrobeItem);
        void DeleteWardrobeItem(Guid id);
    }
}
