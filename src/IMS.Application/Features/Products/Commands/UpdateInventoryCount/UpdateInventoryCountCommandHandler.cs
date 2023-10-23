using IMS.Application.Exceptions;
using IMS.Application.Repositories;
using IMS.Domain.Entities;
using IMS.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.Features.Products.Commands.UpdateInventoryCount
{
    public sealed class UpdateInventoryCountCommandHandler : IRequestHandler<UpdateInventoryCountCommand>
    {
        private readonly IProductRepository _productRepository;

        public UpdateInventoryCountCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Handle(UpdateInventoryCountCommand request, CancellationToken cancellationToken)
        {
            var productToUpdate = await _productRepository.GetById(request.Id);

            if (productToUpdate == null)
            {
                throw new IMSNotFoundException($"Product {request.Id} is not found");
            }

            var validator = new UpdateInventoryCountCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new IMSValidationException(validationResult,"Update inventory count command is not valid");

            if (Convert.ToBase64String(productToUpdate.RowVersion) != Convert.ToBase64String(productToUpdate.RowVersion))
            {
                throw new BadRequestException($"Product {request.Id} has been updated and you did not read last version");
            }

            productToUpdate.UpdateInventoryCount(request.Value);

            await _productRepository.Update(productToUpdate);
        }
    }
}
