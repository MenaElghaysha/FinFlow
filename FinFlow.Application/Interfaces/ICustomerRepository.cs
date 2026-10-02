using FinFlow.Domain.Entities.Customers;

namespace FinFlow.Application.Interfaces;

public interface ICustomerRepository
{
    Task AddAsync(Customer customer);
    Task SaveChangesAsync();
}