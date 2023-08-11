using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
	public class User
	{

		public User()
		{
			
			ShopLists = new HashSet<ShopList>();
		}
		public int UserID { get; set; }
		public string UserName { get; set; } = null!;
		public string UserSurname { get; set; }= null!;
		public string Email { get; set; } = null!; //uniq olmmalı
		public string Password { get; set; }= null!;
		//8 karakter olmalı büyük küçük harf ve rakam içermeli
        public string ConfirmPassword { get; set; } = null!;
        public string Role { get; set; } = null!;


        public ICollection<ShopList> ShopLists { get; set; }

	

		



	}
}
