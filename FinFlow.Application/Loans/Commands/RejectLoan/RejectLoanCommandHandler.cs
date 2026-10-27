using FinFlow.Application.Common.Interfaces;
using FinFlow.Domain.Entities.Loans;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinFlow.Application.Loans.Commands.RejectLoan;

public sealed class RejectLoanCommandHandler(IFinFlowDbContext db)
    : IRequestHandler<RejectLoanCommand>
{
    public async Task Handle(RejectLoanCommand request, CancellationToken ct)
    {
        var loan = await db.LoanApplications
            .FirstOrDefaultAsync(l => l.Id == request.LoanId, ct);

        if (loan is null)
            throw new KeyNotFoundException("Loan not found");

        if (loan.Status != LoanStatus.Pending)
            throw new InvalidOperationException("Only pending loans can be rejected");

        loan.Status = LoanStatus.Rejected;

        await db.SaveChangesAsync(ct);
    }
}