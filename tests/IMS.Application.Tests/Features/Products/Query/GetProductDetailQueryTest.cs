using IMS.Application.Features.Products.Queries.GetProductDetail;
using IMS.Application.Repositories;
using IMS.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace IMS.Application.Tests.Features.Products.Query
{
    public class GetProductDetailQueryTest
    {
        [Fact]
        public async Task ShuoldNotHitRepositoryWhenProductIsInCache()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();

            var command = new GetProductDetailQuery()
            {
                Id = 1
            };

            var cachedProduct = GetCachedProduct();
            var cache = new MemoryCache(new MemoryCacheOptions());
            cache.Set($"product_cache_{command.Id}", cachedProduct);

            var handler = new GetProductDetailQueryHandler(mockRepository.Object, cache);

            // Act            
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(result.Title, cachedProduct.Title);
            mockRepository.Verify(x => x.GetById(It.IsAny<int>()), Times.Never);
        }

        private static Product GetCachedProduct()
        {
            return new Product("Vivobook 16 R1605ZA", 1000, 10);
        }
    }
}
