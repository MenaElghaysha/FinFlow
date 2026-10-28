using MediatR;

namespace FinFlow.Application.Customers.Queries.GetAllCustomers;

public sealed record GetAllCustomersQuery() : IRequest<List<CustomerDto>>;

public sealed record CustomerDto(
    Guid Id,
    string FullName,
    string Email,
    string NationalId,
    decimal MonthlyIncome
);