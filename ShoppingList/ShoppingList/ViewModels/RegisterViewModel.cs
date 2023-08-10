namespace ShoppingList.ViewModels
{
    public class RegisterViewModel
    {
        //public int UserID { get; set; }

        public string UserName { get; set; }     
        public string UserSurname { get; set; }       
        public string Email { get; set; }  //uniq olmmalı
        public string Password { get; set; } = null!;
        //8 karakter olmalı büyük küçük harf ve rakam içermeli
        public string ConfirmPassword { get; set; } 
    }
}
