namespace ShoppingList.ViewModels
{
    public class ProductAddViewModel
    {
        public string ProductName { get; set; } = null!; //uniq olmalı
        public IFormFile? Image { get; set; } //ikon kullanabilirsin belli bir markayı işaret etmemeli
        public string  CategoryName { get; set; }
    }
}
