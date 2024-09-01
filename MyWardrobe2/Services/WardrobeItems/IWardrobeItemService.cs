using MyWardrobe.Models;

namespace MyWardrobe.Services.WardrobeItems
{
    public interface IWardrobeItemService
    {
        void CreateWardrobeItem(WardrobeItem wardrobeItem);
        List<WardrobeItem> GetWardrobeItems();
        WardrobeItem GetWardrobeItem(Guid id);
        
    }
}
