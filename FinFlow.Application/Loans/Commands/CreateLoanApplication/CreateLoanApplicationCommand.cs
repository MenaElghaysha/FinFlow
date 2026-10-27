using MediatR;

namespace FinFlow.Application.Loans.Commands.CreateLoanApplication;

public sealed record CreateLoanApplicationCommand(
    Guid CustomerId,
    decimal Amount,
    int DurationInMonths,
    string Purpose
) : IRequest<Guid>;