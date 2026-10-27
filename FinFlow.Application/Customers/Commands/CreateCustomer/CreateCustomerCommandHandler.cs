using FinFlow.Application.Common.Interfaces;
using FinFlow.Domain.Entities.Customers;
using MediatR;

namespace FinFlow.Application.Customers.Commands.CreateCustomer;

public sealed class CreateCustomerCommandHandler(IFinFlowDbContext db)
    : IRequestHandler<CreateCustomerCommand, Guid>
{
    public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken ct)
    {
        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            FullName = request.FullName,
            Email = request.Email,
            NationalId = request.NationalId,
            MonthlyIncome = request.MonthlyIncome
        };

        db.Customers.Add(customer);
        await db.SaveChangesAsync(ct);

        return customer.Id;
    }
}