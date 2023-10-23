using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.Features.Products.Queries.GetProductDetail
{
    public sealed class ProductDetailVm
    {
        private readonly decimal _price;
        private readonly byte _discount;
        public ProductDetailVm(decimal price,byte discount) 
        { 
            _price = price;
            _discount = discount;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price 
        {
            get
            {
                decimal discountValue = _price * ( _discount / 100m);
                decimal priceAfterDiscount = _price - discountValue;
                return  priceAfterDiscount;
            }
        }
        public byte[] RowVersion { get; set; }
    }
}
