using IMS.Application.Exceptions;
using IMS.Application.Repositories;
using IMS.Application.Services;
using IMS.Domain.Entities;
using MediatR;

namespace IMS.Application.Features.Orders.Commands.CreateOrder
{
    public sealed class CreateOrdertCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IOrderService _orderService;

        public CreateOrdertCommandHandler(IProductRepository productRepository,
                                          IUserRepository userRepository,
                                          IOrderService orderService)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
            _orderService = orderService;
        }
        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateOrderCommandValidator(_productRepository, _userRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new IMSValidationException(validationResult, "create order command is not valid");

            var order = ConvertCommandToOrder(request);

            order = await _orderService.Create(order);

            return order.Id;
        }

        private Order ConvertCommandToOrder(CreateOrderCommand command)
        {
            return new Order(command.ProductId,
                             command.BuyerId);
        }
    }
}
