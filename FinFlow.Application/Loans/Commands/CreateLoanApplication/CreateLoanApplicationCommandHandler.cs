using FinFlow.Application.Common.Interfaces;
using FinFlow.Domain.Entities.Loans;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinFlow.Application.Loans.Commands.CreateLoanApplication;

public sealed class CreateLoanApplicationCommandHandler(IFinFlowDbContext db)
    : IRequestHandler<CreateLoanApplicationCommand, Guid>
{
    public async Task<Guid> Handle(CreateLoanApplicationCommand request, CancellationToken ct)
    {
        // Optional: check if customer exists
        var customerExists = await db.Customers
            .AnyAsync(c => c.Id == request.CustomerId, ct);

        if (!customerExists)
            throw new KeyNotFoundException("Customer not found");

        var loan = new LoanApplication
        {
            Id = Guid.NewGuid(),
            CustomerId = request.CustomerId,
            Amount = request.Amount,
            DurationInMonths = request.DurationInMonths,
            Purpose = request.Purpose,
            Status = LoanStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        db.LoanApplications.Add(loan);
        await db.SaveChangesAsync(ct);

        return loan.Id;
    }
}