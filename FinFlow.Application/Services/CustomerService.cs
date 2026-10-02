using FinFlow.Application.DTOs.Customers;
using FinFlow.Application.Interfaces;
using FinFlow.Domain.Entities.Customers;

namespace FinFlow.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Guid> CreateCustomerAsync(CreateCustomerRequest request)
    {
        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            FullName = request.FullName,
            Email = request.Email,
            NationalId = request.NationalId,
            MonthlyIncome = request.MonthlyIncome
        };

        await _customerRepository.AddAsync(customer);
        await _customerRepository.SaveChangesAsync();

        return customer.Id;
    }
}