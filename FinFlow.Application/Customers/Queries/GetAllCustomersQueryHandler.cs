using FinFlow.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinFlow.Application.Customers.Queries.GetAllCustomers;

public sealed class GetAllCustomersQueryHandler(IFinFlowDbContext db)
    : IRequestHandler<GetAllCustomersQuery, List<CustomerDto>>
{
    public async Task<List<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken ct)
    {
        return await db.Customers
            .AsNoTracking()
            .Select(c => new CustomerDto(
                c.Id,
                c.FullName,
                c.Email,
                c.NationalId,
                c.MonthlyIncome
            ))
            .ToListAsync(ct);
    }
}