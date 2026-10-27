using FinFlow.Application.Common.Interfaces;
using FinFlow.Application.Loans.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinFlow.Application.Loans.Queries.GetAllLoans;

public sealed class GetAllLoansQueryHandler(IFinFlowDbContext db)
    : IRequestHandler<GetAllLoansQuery, List<LoanApplicationDto>>
{
    public async Task<List<LoanApplicationDto>> Handle(GetAllLoansQuery request, CancellationToken ct)
    {
        var loans = await db.LoanApplications
            .AsNoTracking()
            .Include(l => l.Installments)
            .OrderByDescending(l => l.CreatedAt)
            .ToListAsync(ct);

        return loans.Select(loan => new LoanApplicationDto(
            loan.Id,
            loan.CustomerId,
            loan.Amount,
            loan.DurationInMonths,
            loan.Purpose,
            loan.Status.ToString(),
            loan.CreatedAt,
            loan.Installments
                .OrderBy(i => i.DueDate)
                .Select(i => new InstallmentDto(
                    i.Id,
                    i.DueDate,
                    i.Amount,
                    i.IsPaid,
                    i.PaidAt
                )).ToList()
        )).ToList();
    }
}