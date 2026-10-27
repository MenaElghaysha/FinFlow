using FluentValidation;

namespace FinFlow.Application.Loans.Commands.ApproveLoan;

public sealed class ApproveLoanCommandValidator : AbstractValidator<ApproveLoanCommand>
{
    public ApproveLoanCommandValidator()
    {
        RuleFor(x => x.LoanId)
            .NotEmpty().WithMessage("LoanId is required");
    }
}