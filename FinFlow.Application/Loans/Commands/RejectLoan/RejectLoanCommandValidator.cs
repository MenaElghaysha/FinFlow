using FluentValidation;

namespace FinFlow.Application.Loans.Commands.RejectLoan;

public sealed class RejectLoanCommandValidator : AbstractValidator<RejectLoanCommand>
{
    public RejectLoanCommandValidator()
    {
        RuleFor(x => x.LoanId)
            .NotEmpty().WithMessage("LoanId is required");
    }
}