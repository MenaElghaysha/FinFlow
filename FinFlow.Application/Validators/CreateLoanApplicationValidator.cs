using FinFlow.Application.DTOs.Loans;
using FluentValidation;

namespace FinFlow.Application.Validators;

public class CreateLoanApplicationRequestValidator : AbstractValidator<CreateLoanApplicationRequest>
{
    public CreateLoanApplicationRequestValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("CustomerId is required");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than 0");

        RuleFor(x => x.DurationInMonths)
            .InclusiveBetween(1, 60).WithMessage("Duration must be between 1 and 60 months");

        RuleFor(x => x.Purpose)
            .NotEmpty().WithMessage("Purpose is required")
            .MaximumLength(200).WithMessage("Purpose cannot exceed 200 characters");
    }
}