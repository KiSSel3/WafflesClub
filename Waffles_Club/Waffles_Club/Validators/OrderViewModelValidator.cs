using FluentValidation;
using Waffles_Club.Shared.ViewModels;

namespace Waffles_Club.Validators
{
    public class OrderViewModelValidator : AbstractValidator<OrderViewModel>
    {
        public OrderViewModelValidator()
        {
            RuleFor(order => order.WaffleId).NotEmpty().WithMessage("WaffleId не может быть пустым");
            RuleFor(order => order.Count).GreaterThan(0).WithMessage("Count должен быть больше нуля");
        }
    }
}
