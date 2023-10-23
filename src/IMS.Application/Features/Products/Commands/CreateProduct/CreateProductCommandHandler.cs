using IMS.Application.Exceptions;
using IMS.Application.Repositories;
using IMS.Domain.Entities;
using MediatR;

namespace IMS.Application.Features.Products.Commands.CreateProduct
{
    public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateProductCommandValidator(_productRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new IMSValidationException(validationResult, "create product command is not valid");

            var product = ConvertCommandToProduct(request);

            product = await _productRepository.Create(product);

            return product.Id;
        }

        private Product ConvertCommandToProduct(CreateProductCommand command)
        {
            return new Product(command.Title,
                               command.Price,
                               command.Discount);
        }
    }
}
