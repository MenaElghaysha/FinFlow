using MediatR;

namespace FinFlow.Application.Loans.Commands.RejectLoan;

public sealed record RejectLoanCommand(Guid LoanId) : IRequest;