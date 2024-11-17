using FluentValidation;
using MyWardrobe.Contracts.WardrobeItem;

namespace MyWardrobe.Validations
{
    public class CreateWardrobeItemRequestValidator : AbstractValidator<CreateWardrobeItemRequest>
    {
        public CreateWardrobeItemRequestValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Kategori-fältet får inte vara tomt.")
                .Length(1, 100).WithMessage("Kategori-fältet måste vara mellan 1 och 100 tecken.");

            RuleFor(x => x.Subcategory)
                .NotEmpty().WithMessage("Underkategori-fältet får inte vara tomt.")
                .Length(1, 100).WithMessage("Underkategori-fältet måste vara mellan 1 och 100 tecken.");

            RuleFor(x => x.Brand)
                .NotEmpty().WithMessage("Märkes-fältet får inte vara tomt.")
                .Length(1, 100).WithMessage("Märkes-fältet måste vara mellan 1 och 100 tecken.");

            RuleFor(x => x.Model)
                .NotEmpty().WithMessage("Modell-fältet får inte vara tomt.")
                .Length(1, 100).WithMessage("Modell-fältet måste vara mellan 1 och 100 tecken.");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Pris måste vara 0 eller högre.")
                .LessThanOrEqualTo(500000).WithMessage("Pris får inte överstiga 500 000.")
                .PrecisionScale(6, 2, true).WithMessage("Pris får inte innehålla mer än två decimaler.");

            RuleFor(x => x.Material)
                .Length(1, 100).WithMessage("Material-fältet måste vara mellan 1 och 100 tecken.");

            RuleFor(x => x.Color)
                .Length(1, 50).WithMessage("Färg-fältet måste vara mellan 1 och 50 tecken.");

            RuleFor(x => x.Size)
                .Length(1, 30).WithMessage("Storleks-fältet måste vara mellan 1 och 30 tecken.");

            RuleFor(x => x.Description)
                .Length(1, 300).WithMessage("Beskrivning-fältet måste vara mellan 1 och 300 tecken.");
        }
    }
}