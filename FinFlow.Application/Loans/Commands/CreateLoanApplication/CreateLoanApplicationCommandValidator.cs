using FluentValidation;

namespace FinFlow.Application.Loans.Commands.CreateLoanApplication;

public class CreateLoanApplicationCommandValidator
    : AbstractValidator<CreateLoanApplicationCommand>
{
    public CreateLoanApplicationCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("CustomerId is required");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than 0");

        RuleFor(x => x.DurationInMonths)
            .InclusiveBetween(1, 60).WithMessage("Duration must be between 1 and 60 months");

        RuleFor(x => x.Purpose)
            .NotEmpty().WithMessage("Purpose is required")
            .MaximumLength(500).WithMessage("Purpose cannot exceed 500 characters");
    }
}