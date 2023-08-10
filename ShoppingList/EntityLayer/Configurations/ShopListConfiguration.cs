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
	public  class ShopListConfiguration:IEntityTypeConfiguration<ShopList>
	{
		public void Configure(EntityTypeBuilder<ShopList> builder)
		{
			builder.HasIndex(x => x.ListID);
			builder.Property(x => x.ListID).IsRequired().UseIdentityColumn();
			builder.Property(x => x.ListName).HasMaxLength(40).IsRequired();

			builder.HasKey(x => x.ListID);


		}
	}
}
