using FluentValidation;
using IMS.Application.Repositories;

namespace IMS.Application.Features.Products.Commands
{
    public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        private readonly IProductRepository _productRepository;
        public CreateProductCommandValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;

            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(40).WithMessage("{PropertyName} must not exceed 40 characters.")
                .MustAsync(IsProductTitleUnique)
                .WithMessage("Product's title already exists."); ;

            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0.0m);

            RuleFor(p => (int)p.Discount)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .ExclusiveBetween(0, 100);
        }

        private async Task<bool> IsProductTitleUnique(string title, CancellationToken token)
        {
            return !(await _productRepository.IsProductTitleUnique(title));
        }
    }
}
