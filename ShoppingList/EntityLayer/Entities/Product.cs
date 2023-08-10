using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
	public class Product
	{
		public Product()
		{
			ListProductDetails = new HashSet<ListProductDetail>();
		}
		public int ProductID { get; set; }
		public string ProductName { get; set; } = null!; //uniq olmalı
		public string? Image { get; set; } //ikon kullanabilirsin belli bir markayı işaret etmemeli
		
		public int CategoryID { get; set; }
	

	    
		public Category Category { get; set; } //her ürünün bir ketegorisi var
		public ICollection<ListProductDetail> ListProductDetails { get; set; }


	}
}
