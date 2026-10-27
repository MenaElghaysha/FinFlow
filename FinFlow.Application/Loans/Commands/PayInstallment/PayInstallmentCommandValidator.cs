using FluentValidation;

namespace FinFlow.Application.Loans.Commands.PayInstallment;

public sealed class PayInstallmentCommandValidator : AbstractValidator<PayInstallmentCommand>
{
    public PayInstallmentCommandValidator()
    {
        RuleFor(x => x.InstallmentId)
            .NotEmpty().WithMessage("InstallmentId is required");
    }
}