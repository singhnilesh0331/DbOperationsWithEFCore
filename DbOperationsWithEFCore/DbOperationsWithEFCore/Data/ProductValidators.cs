using FluentValidation;

namespace DbOperationsWithEFCore.Data
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(100);
            RuleFor(p => p.Price).GreaterThan(0);
        }
    }
}
