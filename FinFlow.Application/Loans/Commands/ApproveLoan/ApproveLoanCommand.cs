using MediatR;

namespace FinFlow.Application.Loans.Commands.ApproveLoan;

public sealed record ApproveLoanCommand(Guid LoanId) : IRequest;