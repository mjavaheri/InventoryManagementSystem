using IMS.Domain.Exceptions;

namespace IMS.Domain.Entities
{
    public sealed class Order
    {
        public int Id { get; private set; }
        public int ProductId { get; private set; }
        public Product Product { get; private set; }
        public DateTime CreationDate { get; private set; }
        public int BuyerId { get; set; }
        public User Buyer { get; set; }

        public Order(int productId,int buyerId)
        {
            ValidateProductId(productId);
            ValidateBuyerId(buyerId);

            ProductId = productId;
            BuyerId = buyerId;
            CreationDate = DateTime.UtcNow;
        }

        private void ValidateProductId(int productId)
        {
            if (productId <= 0)
                throw new BadRequestException("Product id is not valid");
        }

        private void ValidateBuyerId(int buyerId)
        {
            if (buyerId <= 0)
                throw new BadRequestException("Buyer id is not valid");
        }
    }
}
