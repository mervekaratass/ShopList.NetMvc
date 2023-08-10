using EntityLayer.Entities;	
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
	public class ListProductDetail
	{
		
		
		public int ListID { get; set; }
		public int ProductID { get; set; }


        public string? Description { get; set; }
        public ShopList  ShopList { get; set; }
		public Product Product { get; set; }
		
	}
}
