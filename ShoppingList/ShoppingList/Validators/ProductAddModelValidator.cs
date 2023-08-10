using FluentValidation;
using ShoppingList.ViewModels;

namespace ShoppingList.Validators
{
    public class ProductAddModelValidator : AbstractValidator<ProductAddViewModel>
    {
        public ProductAddModelValidator()
        {
            RuleFor(a => a.ProductName).NotNull().WithMessage("Ürün adı boş geçilemez").MaximumLength(20)
                .WithMessage("Ürün adı en fazla 20 karakter olabilir");

            RuleFor(a => a.CategoryName).NotNull().WithMessage("Kategori adı boş geçilemez");

        }
    
    }
}
