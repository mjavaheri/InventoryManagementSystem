using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.Domain.Entities;

namespace IMS.Infrastructure.EF.Configs
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Title)
                   .IsRequired()
                   .HasMaxLength(40);

            builder.Property(x => x.RowVersion)
                   .IsRowVersion();

            builder.HasIndex(p => new { p.Title }, "IX_Products_Title");
        }
    }
}
