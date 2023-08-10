using EntityLayer.Entities;
using FluentValidation;

namespace ShoppingList.Validators
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
        RuleFor(a => a.CategoryName).NotNull().WithMessage("Kategori adı boş geçilemez").MaximumLength(30)
            .WithMessage("Kategori adı en fazla 30 karakter olabilir");
            
         }
    
    }
}
