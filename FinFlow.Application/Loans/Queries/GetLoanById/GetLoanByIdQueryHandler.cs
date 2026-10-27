using FinFlow.Application.Common.Interfaces;
using FinFlow.Application.Loans.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinFlow.Application.Loans.Queries.GetLoanById;

public sealed class GetLoanByIdQueryHandler(IFinFlowDbContext db)
    : IRequestHandler<GetLoanByIdQuery, LoanApplicationDto?>
{
    public async Task<LoanApplicationDto?> Handle(GetLoanByIdQuery request, CancellationToken ct)
    {
        var loan = await db.LoanApplications
            .AsNoTracking()
            .Include(l => l.Installments)
            .FirstOrDefaultAsync(l => l.Id == request.LoanId, ct);

        if (loan is null)
            return null;

        return new LoanApplicationDto(
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
        );
    }
}