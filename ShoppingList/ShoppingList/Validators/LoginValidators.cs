using FluentValidation;
using ShoppingList.ViewModels;

namespace ShoppingList.Validators
{
    public class LoginValidators: AbstractValidator<LoginViewModel>
    {
        public LoginValidators() {
            RuleFor(a => a.Email).NotNull().WithMessage("Email boş geçilemez").MaximumLength(20).EmailAddress();
            RuleFor(a => a.Password).NotNull().WithMessage("Şifre boş geçilemez");
            

        }
    }
}
