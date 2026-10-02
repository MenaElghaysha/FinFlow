using FinFlow.Application.Interfaces;
using FinFlow.Domain.Entities.Customers;

namespace FinFlow.Infrastructure.Persistence;

public class CustomerRepository : ICustomerRepository
{
    private readonly FinFlowDbContext _context;

    public CustomerRepository(FinFlowDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Customer customer)
    {
        await _context.Customers.AddAsync(customer);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}