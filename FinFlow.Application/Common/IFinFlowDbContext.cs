using FinFlow.Domain.Entities.Customers;
using FinFlow.Domain.Entities.Loans;
using Microsoft.EntityFrameworkCore;

namespace FinFlow.Application.Common.Interfaces;

public interface IFinFlowDbContext
{
    DbSet<Customer> Customers { get; }
    DbSet<LoanApplication> LoanApplications { get; }
    DbSet<Installment> Installments { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}