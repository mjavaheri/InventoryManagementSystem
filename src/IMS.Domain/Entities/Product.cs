using IMS.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Entities
{
    public class Product
    {
        private const int _defualtInventoryCount = 100;

        public int Id { get; private set; }
        public string Title { get; private set; }
        public int InventoryCount { get; private set; }
        public decimal Price { get; private set; }
        public byte Discount { get; private set; }

        public Product(string title, decimal price, byte discount)
        {
            ValidateTitle(title);
            ValidatePrice(price);
            ValidateDiscount(discount);

            Title = title;
            Price = price;
            Discount = discount;
            InventoryCount = _defualtInventoryCount;
        }

        private void ValidateTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new BadRequestException("Product's title can not be null or whitespace");

            if(title.Length > 40)
                throw new BadRequestException("Product's title can not be greater than 40");
        }

        private void ValidatePrice(decimal price)
        {
            if (price < 0.0m)
                throw new BadRequestException("Product's price is not valid");
        }

        private void ValidateDiscount(byte discount)
        {
            if (discount < 0 || discount > 100)
                throw new BadRequestException("Product's discount is not valid");
        }

    }
}
