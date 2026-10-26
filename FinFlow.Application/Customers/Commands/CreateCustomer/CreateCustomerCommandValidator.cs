using FluentValidation;

namespace FinFlow.Application.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("FullName is required")
            .MaximumLength(100).WithMessage("FullName cannot exceed 100 characters");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");

        RuleFor(x => x.NationalId)
            .NotEmpty().WithMessage("NationalId is required")
            .Length(14).WithMessage("NationalId must be 14 digits");

        RuleFor(x => x.MonthlyIncome)
            .GreaterThan(0).WithMessage("MonthlyIncome must be greater than 0");
    }
}