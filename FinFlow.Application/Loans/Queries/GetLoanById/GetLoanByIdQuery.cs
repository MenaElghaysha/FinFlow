using FinFlow.Application.Loans.DTOs;
using MediatR;

namespace FinFlow.Application.Loans.Queries.GetLoanById;

public sealed record GetLoanByIdQuery(Guid LoanId) : IRequest<LoanApplicationDto?>;