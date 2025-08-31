using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWardrobe.Contracts.WardrobeItem;
using MyWardrobe.Models;
using MyWardrobe.Services.WardrobeItemsUsage;
using MyWardrobe.ErrorHandling;

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
        public async Task<ActionResult> CreateWardrobeItemUsage(Guid id)
        {
            var wardrobeItemUsage = await _wardrobeItemUsageService.CreateWardrobeItemUsage(id);

            //if(!result.IsSuccess) {}   Om Failure läggs till.

            return CreatedAtAction(
                actionName: nameof(GetWardrobeItemUsage),
                routeValues: new { id = wardrobeItemUsage.Value.Id },
                value: wardrobeItemUsage);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetWardrobeItemUsage(Guid id)
        {
            var wardrobeItemUsage = await _wardrobeItemUsageService.GetWardrobeItemUsage(id);

            if (!wardrobeItemUsage.IsSuccess)
            {
                return NotFound(wardrobeItemUsage.Error);
            }

            return Ok(wardrobeItemUsage);
        }
    }
}
