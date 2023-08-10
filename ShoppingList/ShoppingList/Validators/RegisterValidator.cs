using FluentValidation;
using ShoppingList.ViewModels;

namespace ShoppingList.Validators
{
    public class RegisterValidator: AbstractValidator<RegisterViewModel>
    {
        public RegisterValidator()
        {
            RuleFor(a => a.UserName).NotNull().WithMessage("İsim boş geçilemez").MaximumLength(30);
            RuleFor(a => a.UserSurname).NotNull().WithMessage("Soyisim boş geçilemez").MaximumLength(30);
            RuleFor(a => a.Email).NotNull().WithMessage("E mail boş geçilemez").MaximumLength(20).EmailAddress();
            RuleFor(a => a.Password).NotNull().WithMessage("Şifre boş geçilemez").MaximumLength(20).Length(8).WithMessage("şifre 8 karakterden oluşmalıdır.").Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$").WithMessage("Şifre en az bir büyük harf, bir küçük harf ve bir rakam içermelidir.");
            RuleFor(a => a.ConfirmPassword).NotNull().WithMessage("Şifre tekrar boş geçilemez").MaximumLength(20).Length(8).Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$").WithMessage("Şifre en az bir büyük harf, bir küçük harf ve bir rakam içermelidir.");
            RuleFor(a => a.ConfirmPassword).Matches(a => a.Password).WithMessage("Şifre ile eşleşmiyor");

        }
    
    }
}
