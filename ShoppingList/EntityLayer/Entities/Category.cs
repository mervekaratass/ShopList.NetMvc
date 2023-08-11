using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
	public class Category
	{
		public Category() { 
		 
			Products=new HashSet<Product>();
		
		}
		public int CategoryID { get; set; }
		public string CategoryName { get; set; } = null!; //uniq olmalı

		public ICollection<Product> Products { get; set; }	

		
	}
}
