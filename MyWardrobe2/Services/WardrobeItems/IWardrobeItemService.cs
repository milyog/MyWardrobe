using MyWardrobe.Contracts.WardrobeItem;
using MyWardrobe.ErrorHandling;
using MyWardrobe.Models;

namespace MyWardrobe.Services.WardrobeItems
{
    public interface IWardrobeItemService
    {
        Task<Result<WardrobeItem>> CreateWardrobeItem(WardrobeItem wardrobeItem);
        Task<Result<List<WardrobeItem>>> GetWardrobeItems();
        Task<Result<WardrobeItem>> GetWardrobeItem(Guid id);
        Task<Result<WardrobeItem>> UpdateWardrobeItem(Guid id, WardrobeItem wardrobeItem);
        Task<Result> DeleteWardrobeItem(Guid id);
    }
}
