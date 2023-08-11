using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
	public  class ShopList
	{
		public ShopList() 
		{
			
			ListProductDetails = new HashSet<ListProductDetail>();
		
		}

		public int ListID { get; set; }
		public string ListName { get; set; } = null!;	

		public int UserID { get; set; }
		public ICollection<ListProductDetail> ListProductDetails { get; set; }
		public User User { get; set; }

	}
}
