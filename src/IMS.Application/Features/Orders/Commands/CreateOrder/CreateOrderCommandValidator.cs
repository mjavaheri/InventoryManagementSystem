using FluentValidation;
using IMS.Application.Features.Products.Commands.CreateProduct;
using IMS.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.Features.Orders.Commands.CreateOrder
{
    public sealed class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;

        public CreateOrderCommandValidator(IProductRepository productRepository,IUserRepository userRepository)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;

            RuleFor(p => p.ProductId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MustAsync(IsProductExists)
                .WithMessage("Product id is not valid");

            RuleFor(p => p.BuyerId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MustAsync(IsUser)
                .WithMessage("Buyer id is not valid"); ; ;
        }

        private async Task<bool> IsProductExists(int productId, CancellationToken token)
        {
            return await _productRepository.IsProductExists(productId);
        }

        private async Task<bool> IsUser(int userId, CancellationToken token)
        {
            return await _userRepository.IsUserExists(userId);
        }
    }
}
