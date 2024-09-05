using FluentValidation;

namespace Ordering.Applications.Features.OrderLogic.Model
{
    public class OrderModelValidator : AbstractValidator<OrderModel>
    {
        public OrderModelValidator()
        {
            RuleFor(o => o.UserName)
                .NotNull().WithMessage("Please, provied user name.")
                .MinimumLength(11).WithMessage("Please, provied valid email address.")
                .EmailAddress().WithMessage("Please, provied valid email address.");

            RuleFor(o => o.TotalPrice)
                .NotNull().WithMessage("Please, provied total price.")
                .GreaterThan(0).WithMessage("Please, provied total price greater then 0.");

            RuleFor(o => o.Address.FirstName)
                .NotNull().WithMessage("Please, provied first name")
                .MinimumLength(2).WithMessage("Please, provied minimum 2 character")
                .MaximumLength(20).WithMessage("Please, provied maximum 20 character.");

            RuleFor(o => o.Address.LastName)
                .NotNull().WithMessage("Please, provied last name")
                .MinimumLength(2).WithMessage("Please, provied minimum 2 character")
                .MaximumLength(20).WithMessage("Please, provied maximum 20 character.");
        }
    }
}