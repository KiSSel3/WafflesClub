using FluentValidation;
using Waffles_Club.Shared.ViewModels;

namespace Waffles_Club.Validators
{
    public class CartViewModelValidator : AbstractValidator<CartViewModel>
    {
        public CartViewModelValidator()
        {
            RuleFor(cart => cart.UserId).NotEmpty().WithMessage("UserId не может быть пустым");
            RuleFor(cart => cart.WaffleId).NotEmpty().WithMessage("WaffleId не может быть пустым");
        }
    }

}
