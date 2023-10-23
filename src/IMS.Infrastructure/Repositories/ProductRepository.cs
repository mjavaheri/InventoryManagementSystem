using Azure.Core;
using IMS.Application.Repositories;
using IMS.Domain.Entities;
using IMS.Domain.Exceptions;
using IMS.Infrastructure.EF;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace IMS.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMSDbContext _dbContext;

        public ProductRepository(IMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> Create(Product product)
        {
            try
            {
                _dbContext.Add(product);
                await _dbContext.SaveChangesAsync();
                return product;
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("IX_Products_Title"))
                    throw new BadRequestException("Product's title already exists.");

                throw;
            }
        }

        public async Task<Product> GetById(int id)
        {
            return await _dbContext.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> IsProductExists(int productId)
        {
            return await _dbContext.Products.AnyAsync(x => x.Id == productId);
        }

        public async Task<bool> IsProductTitleUnique(string title)
        {
            return await _dbContext.Products.AnyAsync(x => x.Title == title);
        }

        public async Task Update(Product product)
        {
            try
            {
                _dbContext.Entry(product).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new BadRequestException($"Product {product.Id} has been updated and you did not read last version");
            }
        }
    }
}
