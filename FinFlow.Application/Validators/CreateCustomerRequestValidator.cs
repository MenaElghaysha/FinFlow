using FinFlow.Application.DTOs.Customers; 
using FluentValidation;

namespace FinFlow.Application.Validators;

public class CreateCustomerRequestValidator : AbstractValidator<CreateCustomerRequest>
{
    public CreateCustomerRequestValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("FullName is required")
            .MaximumLength(100).WithMessage("FullName cannot exceed 100 characters");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");

        RuleFor(x => x.NationalId)
            .NotEmpty().WithMessage("NationalId is required")
            .Length(14).WithMessage("NationalId must be 14 digits"); // لو الرقم القومي المصري

        RuleFor(x => x.MonthlyIncome)
            .GreaterThan(0).WithMessage("MonthlyIncome must be greater than 0");
    }
}