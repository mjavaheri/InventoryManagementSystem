using IMS.Application.Exceptions;
using IMS.Application.Repositories;
using IMS.Domain.Entities;
using MediatR;

namespace IMS.Application.Features.Products.Queries.GetProductDetail
{
    public sealed class GetProductDetailQueryHandler : IRequestHandler<GetProductDetailQuery, ProductDetailVm>
    {
        private readonly IProductRepository _productRepository;

        public GetProductDetailQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDetailVm> Handle(GetProductDetailQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(request.Id);

            if (product == null)
            {
                throw new IMSNotFoundException($"Product {request.Id} is not found");
            }

            return ConvertProductToProductDetailVm(product);
        }

        private ProductDetailVm ConvertProductToProductDetailVm(Product product)
        {
            return new ProductDetailVm(product.Price, product.Discount)
            {
                Id = product.Id,
                RowVersion = product.RowVersion,
                Title = product.Title
            };
        }
    }
}
