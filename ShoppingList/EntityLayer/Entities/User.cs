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
			//ListProductDetails = new HashSet<ListProductDetail>();
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

		//public ICollection<ListProductDetail> ListProductDetails { get; set; }

		//Notlar
		//--->ÜYE OLMA
		//--->kullanıcı sisteme kaydolmalı yoksa giriş yapamaz ve kullanamaz
		//--->ad,soyad,email,şifre,şifre tekrar zorunlu alanlar.
		//--->her mail adresine sadece bir kullanıcı hesabı tanımlı
		//kullanıcı bilgileri girdikten sonra üyeliği tamamla diyerek kaydı gerçekleştiri

		//ÜYE GİRİŞ YAPMA
		//--->sisteme giriş email ve şifre ile olmalı
		//--->Admin kullanıcısı ise sistem ayağa kaldırılırken oluşturulan mail ve şifre 
		//--->bilgileriyle sisteme giriş yapar.
		//--->her mail adresine sadece bir kullanıcı hesabı tanımlı


		//---->ADMİN KULLANICISI OLMA
		//--->admin ürün ve kategorilerle ile lgili işlemleri yürütür
		//--->kategori ekleme,güncelleme,kaldırma
		//--->ürün ekleme,güncelleme ve kaldırma işlemlerini yapar.

		//KATEGORİ
		//--->Kategorilerin sadece adı var bir ad bir kategoriyi tanımlar

		//ÜRÜNLER
		//--->Ürünlerin adı ve görseli var.
		//--->her ürünün bir kategorisi var
		//--->sistemde aynı isimde tek bir ürün olur
		//--->ürün görseli belli bir markayı işaret etmemelidir görsel olarak ikon kullanılabilir.

		//LİSTE OLUŞTURMA VE ÜRÜN EKLEME
		//--->Üyeler istedikleri kadar liste oluşturabilir.
		//--->Listelerin sadece adı var

		//--->Ürün listesinden ürün seçerek ürün eklerler.
		//--->Seçilen ürün için "isterlerse" açıklma girebilirler.

		//Listelerini görüntülediklerinde sadece adını ve kayıtlı görselini görürler.
		//İsterlerse detayına gidip ürün eklerken girdikleri açıklamayı görebilirler ve buradan bu bilgiyi değiştirebilir.
		//Listede görüntülenen ürünlerden istediklerini kaldırabilirler

		//ALIŞVERİŞ YAPMA
		//Alışverişe çıktığında Alışverişe çıkıyorum seçeneğini işaretlerler.
		//Bu işaretlemeden sonra liste üzeinde oyanama yapamazlar.Ürünekleyemzler.Sadece görüntülerler.

		//Alışverişe çıkan üye ürünleri aldıkça aldım şeklinde işaretleyebilir.

		//alışveriş bitttiğinde liste için alışvveriş tamamlandı seçeneğini iaretler.
		//Böylece liste üzerinde yeniden eklemeisilmei güncelleme işlemleri yapabilir.
		//Ayrıca aldımm seçeneği işaretli olan ürünler otomatik listeden kaldırılır.

		//LİSTE SİLME
		//Üyeler kendi oluşturdujları listeleri istedikleri zman silebilir.


		//sisteme kayıt ekranı
		//giriş ekranı
		//listeler ekranı(oluşturdukları alışveriş listelerini görecek.Buradan 
		//istedikleri listeyi seçip ürün ekleme ekrana geçebileceklerdir


		//Ürün ekleme ekranı (Ürün ekleyebilmeleri için listelenen ürünlerden istediklerine tıklayıp
		//beklenen bilgileri girecek ve ekle diyeceklerdir.
		//ürünlerin içerisinde ismiyle arama yapabilecek ve isterse kategoriye göre filtreleyecek
		//listeleri ürünleri ekleyerek değil kaldırarak da güncelleyebilecek



	}
}
