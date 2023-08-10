using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EntityLayer.Context
{
	public class ShoppingDbContext : DbContext
	{

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
		
			optionsBuilder.UseSqlServer("Server=.;Database=ShoppingDB;Trusted_Connection=True;Encrypt=False");
		}


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			//modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("03_Relationships"));
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}


		// dbset
		public DbSet<User> Users { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ShopList> ShopLists { get; set; }
		public DbSet<Category> Categories { get; set; }

		public DbSet<ListProductDetail> ListProductDetails{ get; set; }

	}

}

