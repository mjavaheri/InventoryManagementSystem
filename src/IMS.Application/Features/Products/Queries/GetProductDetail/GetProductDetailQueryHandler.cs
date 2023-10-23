using IMS.Application.Exceptions;
using IMS.Application.Repositories;
using IMS.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace IMS.Application.Features.Products.Queries.GetProductDetail
{
    public sealed class GetProductDetailQueryHandler : IRequestHandler<GetProductDetailQuery, ProductDetailVm>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMemoryCache _cache;

        public GetProductDetailQueryHandler(IProductRepository productRepository,
                                            IMemoryCache cache)
        {
            _productRepository = productRepository;
            _cache = cache;
        }

        public async Task<ProductDetailVm> Handle(GetProductDetailQuery request, CancellationToken cancellationToken)
        {
            var productCacheKey = $"product_cache_{request.Id}";

            if (!_cache.TryGetValue(productCacheKey, out Product product))
            {
                product = await _productRepository.GetById(request.Id);

                if (product == null)
                {
                    throw new IMSNotFoundException($"Product {request.Id} is not found");
                }

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                    .SetPriority(CacheItemPriority.Normal);

                _cache.Set(productCacheKey, product, cacheEntryOptions);
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
