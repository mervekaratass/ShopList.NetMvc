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
	public class UserConfiguartion : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.HasIndex(x => x.UserID);
			builder.Property(x => x.UserID).IsRequired().UseIdentityColumn();
			builder.Property(x => x.UserName).HasMaxLength(30).IsRequired();
			builder.Property(x => x.UserSurname).HasMaxLength(30).IsRequired();
			builder.Property(x=>x.Email).HasMaxLength(20).IsRequired();
			builder.HasIndex(x => x.Email).IsUnique();
			builder.Property(x => x.Password).HasMaxLength(8).IsRequired();
			builder.Property(x => x.ConfirmPassword).HasMaxLength(8).IsRequired();
            builder.Property(x => x.Role).HasMaxLength(10).IsRequired().HasDefaultValue("User");



            builder.HasCheckConstraint("CK_User_Password", "[Password] LIKE '%[0-9]%' AND [Password] LIKE '%[A-Z]%' AND [Password] LIKE '%[a-z]%'");

			builder.HasCheckConstraint("CK_User_ConfirmPassword", "[ConfirmPassword] LIKE '%[0-9]%' AND [ConfirmPassword] LIKE '%[A-Z]%' AND [ConfirmPassword] LIKE '%[a-z]%'");

            builder.HasData(new User()
            {
                UserID = 1,
                UserName = "Johannes",
                UserSurname = "Kepler",
                Email = "admin@hotmail.com",
                Password = "Johan123",
                ConfirmPassword = "Johan123",
                Role = "Admin"
            });

            builder.HasMany(a => a.ShopLists).WithOne(a => a.User).OnDelete(DeleteBehavior.Cascade);


		}
	}
}
