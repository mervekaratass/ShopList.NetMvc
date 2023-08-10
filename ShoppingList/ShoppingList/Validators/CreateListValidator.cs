using FluentValidation;
using ShoppingList.ViewModels;

namespace ShoppingList.Validators
{
    public class CreateListValidator: AbstractValidator<CreateListViewModel>
    {
        public CreateListValidator()
        {
            RuleFor(a => a.ListName).NotNull().WithMessage("Liste adı boş geçilemez").MaximumLength(40)
                .WithMessage("Liste adı en fazla 40 karakter olabilir");
        }
    
    }
}
