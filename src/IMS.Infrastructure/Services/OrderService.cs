using Azure.Core;
using IMS.Application.Services;
using IMS.Domain.Entities;
using IMS.Domain.Exceptions;
using IMS.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace IMS.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMSDbContext _dbContext;

        public OrderService(IMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Order> Create(Order order)
        {
            using var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.RepeatableRead);

            try
            {
                var product = _dbContext.Products.FirstOrDefault(x => x.Id == order.ProductId);

                if (product == null)
                { 
                   throw new BadRequestException($"Product {order.Id} is not valid");
                }

                product.DecreaseInventoryCount();

                _dbContext.Orders.Add(order);

                _dbContext.Entry(product).State = EntityState.Modified;

                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();

                return order;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
