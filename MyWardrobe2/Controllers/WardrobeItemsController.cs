using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWardrobe.Contracts.WardrobeItem;
using MyWardrobe.Models;
using MyWardrobe.Services.WardrobeItems;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyWardrobe.ErrorHandling;

namespace MyWardrobe.Controllers
{
    [Route("api/wardrobe-items")]
    [ApiController]
    public class WardrobeItemsController : ControllerBase
    {
        private readonly IWardrobeItemService _wardrobeItemService;
        private readonly IValidator<CreateWardrobeItemRequest> _createValidator;
        private readonly IValidator<UpdateWardrobeItem> _updateValidator;

        public WardrobeItemsController(
            IWardrobeItemService wardrobeItemService, 
            IValidator<CreateWardrobeItemRequest> createVvalidator,
            IValidator<UpdateWardrobeItem> updateValidator)
        {
            _wardrobeItemService = wardrobeItemService;
            _createValidator = createVvalidator;
            _updateValidator = updateValidator;
        }

        [HttpPost]
        public ActionResult CreateWardrobeItem(CreateWardrobeItemRequest request)
        {   
            ValidationResult validationResult = _createValidator.Validate(request);

            if(!validationResult.IsValid)
            {
                return ValidationErrors(validationResult);
            }

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

            WardrobeItemResponse response = MapWardrobeItemResponse(wardrobeItem);

            return CreatedAtAction(
                actionName: nameof(GetWardrobeItem),
                routeValues: new {id = wardrobeItem.Id},
                value: response);
        }

        [HttpGet]
        public ActionResult GetWardrobeItems()
        {
            List<WardrobeItem> wardrobeItems = _wardrobeItemService.GetWardrobeItems();

            var responseCollection = new List<WardrobeItemResponse>();

            foreach (var item in wardrobeItems)
            {
                var response = MapWardrobeItemResponse(item);

                responseCollection.Add(response);
            }

            return Ok(responseCollection);
        }

        [HttpGet("{id:guid}")]
        public ActionResult GetWardrobeItem(Guid id)
        {
            var wardrobeItem = _wardrobeItemService.GetWardrobeItem(id);

            if (!wardrobeItem.IsSuccess)
            {
                return NotFound(wardrobeItem.Error);
            }
            
            var response = MapWardrobeItemResponse(wardrobeItem);

            return Ok(response);
        }

        [HttpPut("{id:guid}")]
        public ActionResult UpdateWardrobeItem(Guid id, UpdateWardrobeItem request)
        {
            ValidationResult validationResult = _updateValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                return ValidationErrors(validationResult);
            }

            var wardrobeItem = new WardrobeItem(
                id,
                request.Category,
                request.Subcategory,
                request.Brand,
                request.Model,
                request.Price,
                request.Material,
                request.Color,
                request.Size,
                request.Description
                );

            _wardrobeItemService.UpdateWardrobeItem(wardrobeItem);

            return NoContent(); 
        }

        [HttpDelete("{id:guid}")]
        public ActionResult DeleteWardrobeItem(Guid id)
        {
            _wardrobeItemService.DeleteWardrobeItem(id);

            return NoContent();
        }

        private static WardrobeItemResponse MapWardrobeItemResponse(Result<WardrobeItem> wardrobeItem)
        {
            return new WardrobeItemResponse(
                wardrobeItem.Value.Id,
                wardrobeItem.Value.Category,
                wardrobeItem.Value.Subcategory,
                wardrobeItem.Value.Brand,
                wardrobeItem.Value.Model,
                wardrobeItem.Value.Price,
                wardrobeItem.Value.Material,
                wardrobeItem.Value.Color,
                wardrobeItem.Value.Size,
                wardrobeItem.Value.Description,
                wardrobeItem.Value.WardrobeItemUsages
                );
        }

        private static WardrobeItemResponse MapWardrobeItemResponse(WardrobeItem wardrobeItem)
        {
            return new WardrobeItemResponse(
                wardrobeItem.Id,
                wardrobeItem.Category,
                wardrobeItem.Subcategory,
                wardrobeItem.Brand,
                wardrobeItem.Model,
                wardrobeItem.Price,
                wardrobeItem.Material,
                wardrobeItem.Color,
                wardrobeItem.Size,
                wardrobeItem.Description,
                wardrobeItem.WardrobeItemUsages
                );
        }

        private ActionResult ValidationErrors(ValidationResult validationResult)
        {
            var modelStateDictionary = new ModelStateDictionary();

            foreach (ValidationFailure failure in validationResult.Errors)
            {
                modelStateDictionary.AddModelError(
                    failure.PropertyName,
                    failure.ErrorMessage);
            }

            return ValidationProblem(modelStateDictionary);
        }
    }
}
