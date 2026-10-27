using FinFlow.Application.Common.Interfaces;
using FinFlow.Domain.Entities.Loans;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinFlow.Application.Loans.Commands.ApproveLoan;

public sealed class ApproveLoanCommandHandler(IFinFlowDbContext db)
    : IRequestHandler<ApproveLoanCommand>
{
    public async Task Handle(ApproveLoanCommand request, CancellationToken ct)
    {
        var loan = await db.LoanApplications
            .FirstOrDefaultAsync(l => l.Id == request.LoanId, ct);

        if (loan is null)
            throw new KeyNotFoundException("Loan not found");

        if (loan.Status != LoanStatus.Pending)
            throw new InvalidOperationException("Only pending loans can be approved");

        // 1. Change status
        loan.Status = LoanStatus.Approved;

        // 2. Generate installments as a separate list
        var installmentAmount = Math.Round(loan.Amount / loan.DurationInMonths, 2);
        var installments = new List<Installment>();

        for (int i = 1; i <= loan.DurationInMonths; i++)
        {
            installments.Add(new Installment
            {
                Id = Guid.NewGuid(),
                LoanApplicationId = loan.Id,
                Amount = installmentAmount,
                DueDate = loan.CreatedAt.AddMonths(i),
                IsPaid = false
            });
        }

        // 3. Add them directly to the DbSet
        db.Installments.AddRange(installments);

        await db.SaveChangesAsync(ct);
    }
}