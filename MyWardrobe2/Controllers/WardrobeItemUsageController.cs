using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWardrobe.Contracts.WardrobeItem;
using MyWardrobe.Models;
using MyWardrobe.Services.WardrobeItemsUsage;

namespace MyWardrobe.Controllers
{
    [Route("api/wardrobe-items-usage")]
    [ApiController]
    public class WardrobeItemUsageController : ControllerBase
    {
        private readonly IWardrobeItemUsageService _wardrobeItemUsageService;

        public WardrobeItemUsageController(IWardrobeItemUsageService wardrobeItemUsageService)
        {
            _wardrobeItemUsageService = wardrobeItemUsageService;
        }

        [HttpPost("{id:guid}")]
        public ActionResult CreateWardrobeItemUsage(Guid id)
        {
            var wardrobeItemUsage = _wardrobeItemUsageService.CreateWardrobeItemUsage(id);

            return CreatedAtAction(
                actionName: nameof(GetWardrobeItemUsage),
                routeValues: new { id = wardrobeItemUsage.Id },
                value: wardrobeItemUsage);
        }

        [HttpGet("{id:guid}")]
        public ActionResult GetWardrobeItemUsage(Guid id)
        {
            var wardrobeItemUsage = _wardrobeItemUsageService.GetWardrobeItemUsage(id);

            return Ok(wardrobeItemUsage);
        }
    }
}
