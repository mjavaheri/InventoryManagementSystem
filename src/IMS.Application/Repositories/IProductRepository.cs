using IMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.Repositories
{
    public interface IProductRepository
    {
        Task<bool> IsProductTitleUnique(string title);
        Task<Product> Create(Product product);
        Task<Product> GetById(int id);
        Task Update(Product product);
        Task<bool> IsProductExists(int productId);
    }
}
