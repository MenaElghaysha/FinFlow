using FinFlow.Application.DTOs.Customers;

namespace FinFlow.Application.Interfaces;

public interface ICustomerService
{
    Task<Guid> CreateCustomerAsync(CreateCustomerRequest request);
}