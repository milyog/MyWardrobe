using FluentValidation;

namespace MyWardrobe.Validations
{
    public static class WardrobeItemValidatorExtensions
    {
        public static IRuleBuilderOptions<T, string> CategoryRules<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("Kategori-fältet får inte vara tomt.")
                .Length(1, 100).WithMessage("Kategori-fältet måste vara mellan 1 och 100 tecken.");
        }

        public static IRuleBuilderOptions<T, string> SubCategoryRules<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("Underkategori-fältet får inte vara tomt.")
                .Length(1, 100).WithMessage("Underkategori-fältet måste vara mellan 1 och 100 tecken.");
        }

        public static IRuleBuilderOptions<T, string> BrandRules<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("Märkes-fältet får inte vara tomt.")
                .Length(1, 100).WithMessage("Märkes-fältet måste vara mellan 1 och 100 tecken.");
        }

        public static IRuleBuilderOptions<T, string> ModelRules<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("Modell-fältet får inte vara tomt.")
                .Length(1, 100).WithMessage("Modell-fältet måste vara mellan 1 och 100 tecken.");
        }

        public static IRuleBuilderOptions<T, decimal> PriceRules<T>(
            this IRuleBuilder<T, decimal> ruleBuilder)
        {
            return ruleBuilder
                .GreaterThanOrEqualTo(0).WithMessage("Pris måste vara 0 eller högre.")
                .LessThanOrEqualTo(500000).WithMessage("Pris får inte överstiga 500 000.")
                .PrecisionScale(6, 2, true).WithMessage("Pris får inte innehålla mer än två decimaler.");
        }

        public static IRuleBuilderOptions<T, string?> MaterialRules<T>(
            this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                .Length(1, 100)  
                .WithMessage("Material-fältet måste vara mellan 1 och 100 tecken.");
        }

        public static IRuleBuilderOptions<T, string?> ColorRules<T>(
            this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder  
                .Length(1, 50)
                .WithMessage("Färg-fältet måste vara mellan 1 och 50 tecken.");
        }

        public static IRuleBuilderOptions<T, string?> SizeRules<T>(
            this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                .Length(1, 15)
                .WithMessage("Storleks-fältet måste vara mellan 1 och 15 tecken.");
        }

        public static IRuleBuilderOptions<T, string?> DescriptionRules<T>(
            this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                .Length(1, 300)
                .WithMessage("Beskrivning-fältet måste vara mellan 1 och 300 tecken.");
        }
    }
}
