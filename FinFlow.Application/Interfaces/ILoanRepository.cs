using FinFlow.Domain.Entities.Loans;

namespace FinFlow.Application.Interfaces;

public interface ILoanRepository
{
    Task AddAsync(LoanApplication loanApplication);
    Task UpdateAsync(LoanApplication loanApplication);
    Task AddInstallmentsAsync(List<Installment> installments);
    Task SaveChangesAsync();

    Task<List<LoanApplication>> GetAllAsync();
    Task<LoanApplication?> GetByIdAsync(Guid id);
    Task<Installment?> GetInstallmentByIdAsync(Guid id);
}