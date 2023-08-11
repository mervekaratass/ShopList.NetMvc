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

	public class CategoryConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.HasIndex(x => x.CategoryID);
			builder.HasKey(x => x.CategoryID);
			builder.Property(x => x.CategoryID).IsRequired().UseIdentityColumn();
			builder.Property(x => x.CategoryName).HasMaxLength(30).IsRequired();
			builder.HasIndex(x=> x.CategoryName).IsUnique();

			builder.HasMany(a => a.Products)
				.WithOne(a => a.Category)
				.OnDelete(DeleteBehavior.Cascade);

		}
	}
}
