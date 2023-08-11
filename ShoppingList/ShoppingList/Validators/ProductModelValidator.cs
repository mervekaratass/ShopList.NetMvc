using EntityLayer.Entities;
using FluentValidation;
using ShoppingList.ViewModels;

namespace ShoppingList.Validators
{
    public class ProductModelValidator : AbstractValidator<ProductViewModel>
    {
        public ProductModelValidator()
        {
           

            RuleFor(a => a.ProductName).NotNull().WithMessage("Ürün adı boş geçilemez");
            

            RuleFor(a => a.ProductDescription).MaximumLength(120)
               .WithMessage("Açıklama en fazla 120 karakter olabilir");

        }
    
    }
}
