using FluentValidation;
using ShoppingList.ViewModels;

namespace ShoppingList.Validators
{
    public class ProductEditModelValidators : AbstractValidator<ProductEditViewModel>
    {
       
        
            public ProductEditModelValidators()
            {
                RuleFor(a => a.ProductName).NotNull().WithMessage("Ürün adı boş geçilemez").MaximumLength(20)
                    .WithMessage("Ürün adı en fazla 20 karakter olabilir");

                RuleFor(a => a.CategoryName).NotNull().WithMessage("Kategori adı boş geçilemez");

            }

      }
}

