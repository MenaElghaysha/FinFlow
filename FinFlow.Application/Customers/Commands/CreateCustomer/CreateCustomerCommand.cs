using MediatR;

namespace FinFlow.Application.Customers.Commands.CreateCustomer;

public sealed record CreateCustomerCommand(
    string FullName,
    string Email,
    string NationalId,
    decimal MonthlyIncome) : IRequest<Guid>;