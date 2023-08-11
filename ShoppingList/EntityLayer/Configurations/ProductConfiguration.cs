using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Configurations
{
	
	public class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.HasIndex(x => x.ProductID);
			builder.Property(x => x.ProductID).IsRequired().UseIdentityColumn();
			builder.Property(x => x.Image).HasMaxLength(500);
			
			builder.Property(x => x.ProductName).HasMaxLength(20).IsRequired();
			builder.HasIndex(x => x.ProductName).IsUnique();




        }
	}
}
