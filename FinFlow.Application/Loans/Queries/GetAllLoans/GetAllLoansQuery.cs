using FinFlow.Application.Loans.DTOs;
using MediatR;

namespace FinFlow.Application.Loans.Queries.GetAllLoans;

public sealed record GetAllLoansQuery() : IRequest<List<LoanApplicationDto>>;