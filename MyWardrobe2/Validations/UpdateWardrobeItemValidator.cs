using FluentValidation;
using MyWardrobe.Contracts.WardrobeItem;

namespace MyWardrobe.Validations
{
    public class UpdateWardrobeItemValidator : AbstractValidator<UpdateWardrobeItem>
    {
        public UpdateWardrobeItemValidator() 
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Category).CategoryRules();
            RuleFor(x => x.Subcategory).SubCategoryRules();
            RuleFor(x => x.Brand).BrandRules();
            RuleFor(x => x.Model).ModelRules();
            RuleFor(x => x.Price).PriceRules();
            RuleFor(x => x.Material).MaterialRules()
                .When(x => !string.IsNullOrWhiteSpace(x.Material));
            RuleFor(x => x.Color).ColorRules()
                .When(x => !string.IsNullOrWhiteSpace(x.Color));
            RuleFor(x => x.Size).SizeRules()
                .When(x => !string.IsNullOrWhiteSpace(x.Size));
            RuleFor(x => x.Description).DescriptionRules()
                .When(x => !string.IsNullOrWhiteSpace(x.Description));
        }
    }
}
