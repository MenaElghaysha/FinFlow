using FinFlow.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinFlow.Application.Loans.Commands.PayInstallment;

public sealed class PayInstallmentCommandHandler(IFinFlowDbContext db)
    : IRequestHandler<PayInstallmentCommand>
{
    public async Task Handle(PayInstallmentCommand request, CancellationToken ct)
    {
        var installment = await db.Installments
            .FirstOrDefaultAsync(i => i.Id == request.InstallmentId, ct);

        if (installment is null)
            throw new KeyNotFoundException("Installment not found");

        if (installment.IsPaid)
            throw new InvalidOperationException("Installment is already paid");

        installment.IsPaid = true;
        installment.PaidAt = DateTime.UtcNow;

        await db.SaveChangesAsync(ct);
    }
}