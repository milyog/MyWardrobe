using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWardrobe.Contracts.WardrobeItem;
using MyWardrobe.Models;
using MyWardrobe.Services.WardrobeItems;

namespace MyWardrobe.Controllers
{
    [Route("api/wardrobe-items")]
    [ApiController]
    public class WardrobeItemsController : ControllerBase
    {
        private readonly IWardrobeItemService _wardrobeItemService;

        public WardrobeItemsController(IWardrobeItemService wardrobeItemService)
        {
            _wardrobeItemService = wardrobeItemService;
        }

        [HttpPost]
        public ActionResult CreateWardrobeItem(CreateWardrobeItemRequest request)
        {
            var wardrobeItem = new WardrobeItem(
                Guid.NewGuid(),
                request.Category,
                request.Subcategory, 
                request.Brand, 
                request.Model,
                request.Price,
                request.Material, 
                request.Color,
                request.Size,
                request.Description);

            _wardrobeItemService.CreateWardrobeItem(wardrobeItem);

            var response = new WardrobeItemResponse(
                wardrobeItem.Id,
                wardrobeItem.Category,
                wardrobeItem.Subcategory,
                wardrobeItem.Brand,
                wardrobeItem.Model,
                wardrobeItem.Price,
                wardrobeItem.Material,
                wardrobeItem.Color,
                wardrobeItem.Size,
                wardrobeItem.Description);

            return CreatedAtAction(
                actionName: nameof(GetWardrobeItem),
                routeValues: new {id = wardrobeItem.Id},
                value: response);
        }

        [HttpGet]
        public ActionResult GetWardrobeItems()
        {
            List<WardrobeItem> wardrobeItems = _wardrobeItemService.GetWardrobeItems();
            return Ok(wardrobeItems);
        }

        [HttpGet("{id:guid}")]
        public ActionResult GetWardrobeItem(Guid id)
        {
            WardrobeItem wardrobeItem = _wardrobeItemService.GetWardrobeItem(id);
            return Ok(wardrobeItem);
        }

        [HttpPut("{id:guid}")]
        public ActionResult UpdateWardrobeItem(Guid id, UpdateWardrobeItem request)
        {
            return Ok(request);
        }

        [HttpDelete("{id:guid}")]
        public ActionResult DeleteWardrobeItem(Guid id)
        {
            return Ok(id);
        }
    }
}
