using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Configurations
{
	public class ListProductDetailConfiguration : IEntityTypeConfiguration<ListProductDetail>
	{
		public void Configure(EntityTypeBuilder<ListProductDetail> builder)
		{
			
			builder.HasKey(a => new { a.ListID, a.ProductID });
            builder.Property(x => x.Description).HasMaxLength(120);

            builder.HasOne(a => a.ShopList)
					.WithMany(a => a.ListProductDetails).HasForeignKey(x => x.ListID).OnDelete(DeleteBehavior.Cascade); 


			builder.HasOne(a => a.Product)
				.WithMany(a => a.ListProductDetails).HasForeignKey(x => x.ProductID).OnDelete(DeleteBehavior.Cascade);


        



        }
    }
}
