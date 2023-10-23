using IMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMS.Infrastructure.EF.Configs
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.HasOne(e => e.Product)
                .WithMany(e => e.Orders)
                .HasForeignKey(e => e.ProductId)
                .IsRequired();

            builder.HasOne(e => e.Buyer)
                .WithMany(e => e.Orders)
                .HasForeignKey(e => e.BuyerId)
                .IsRequired();
        }
    }
}
